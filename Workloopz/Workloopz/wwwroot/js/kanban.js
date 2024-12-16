fetch('/api/kanban/data')
    .then(response => response.json())
    .then(data => {
        var KanbanTest = new jKanban({
            element: "#myKanban",
            gutter: "10px",
            widthBoard: "450px",
            dragBoards: false,
            dragItems: true,
            itemHandleOptions: {
                enabled: true, // Bật kéo cho các item
            },
            click: function (el) {
                const taskId = el.dataset.eid; // Lấy ID của task khi nhấp vào
                document.getElementById('taskTitle').innerText = el.innerText;
                var taskModal = new bootstrap.Modal(document.getElementById('taskModal'));
               
                 openTaskModal(el);
            },
            context: function (el, e) {
                console.log("Trigger on all items right-click!");
            },
            dropEl: function (el, target, source, sibling) {
                const taskId = el.dataset.eid; // ID của task
                const newStatusId = target.parentElement.getAttribute('data-id'); // ID của board mới
                console.log(`Moved Task ID ${taskId} to Board ID ${newStatusId}`);
                fetch('/api/kanban/updateTask', {
                    method: 'PUT',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ id: taskId, status: newStatusId }),
                })
                    .then(response => {
                        if (!response.ok) {
                            console.error("Failed to update task");
                        }
                    })
                    .catch(error => console.error("Error:", error));
            },
            buttonClick: function (el, boardId) {
                var formItem = document.createElement("form");
                formItem.setAttribute("class", "itemform");
                formItem.innerHTML =
                    '<div class="form-group"><textarea class="form-control" rows="2" autofocus></textarea></div>' +
                    '<div class="form-group">' +
                    '<button type="submit" class="btn btn-primary btn-xs pull-right">Submit</button>' +
                    '<button type="button" id="CancelBtn" class="btn btn-default btn-xs pull-right">Cancel</button>' +
                    '</div>';
                KanbanTest.addForm(boardId, formItem);
                formItem.addEventListener("submit", function (e) {
                    e.preventDefault();
                    var text = e.target[0].value;
                    fetch('/api/kanban/createTask', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({
                            status: boardId,
                            title: text
                        })
                    })
                        .then(response => response.json())
                        .then(res => {
                            KanbanTest.addElement(boardId, {
                                id: res.task.id.toString(),
                                title: res.task.tittle
                            });
                        })
                        .catch(error => console.error('Error:', error));
                    formItem.parentNode.removeChild(formItem);
                });

                document.getElementById("CancelBtn").onclick = function () {
                    formItem.parentNode.removeChild(formItem);
                };
            },
            itemAddOptions: {
                enabled: true,
                content: '+ Add New Card',
                class: 'custom-button',
                footer: true,
            },
            boards: data.map(board => ({
                id: board.id.toString(),
                title: board.title,
                item: board.item.map(task => ({
                    id: task.id.toString(),
                    title: task.title,
                    click: function (el) {
                        const taskId = task.id;
                        console.log('Task clicked: ' + taskId);
                    },
                }))
            }))
        });
    })
    .catch(error => console.error("Error loading boards:", error));

document.addEventListener('DOMContentLoaded', function () {
    const modal = document.getElementById('taskModal');
    const descriptionInput = modal.querySelector('#description');
    const submitBtn = modal.querySelector('#submitBtn');
    const cancelBtn = document.createElement('button');
    cancelBtn.textContent = 'Huỷ';
    cancelBtn.type = 'button';
    cancelBtn.className = 'btn btn-secondary ms-2';
    cancelBtn.style.display = 'none';
    submitBtn.insertAdjacentElement('afterend', cancelBtn);

    descriptionInput.addEventListener('click', () => {
        submitBtn.style.display = 'inline-block';
        cancelBtn.style.display = 'inline-block';
    });

    cancelBtn.addEventListener('click', () => {
        descriptionInput.value = '';
        submitBtn.style.display = 'none';
        cancelBtn.style.display = 'none';
    });

    modal.addEventListener('show.bs.modal', function () {
        submitBtn.style.display = 'none';
        cancelBtn.style.display = 'none';
    });
});
// Kết nối SignalR
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

// Nhận bình luận mới từ server
connection.on("ReceiveMessage", (user, message, taskId) => {
    const currentTaskId = document.getElementById("taskModal").getAttribute("data-task-id");
    if (currentTaskId === taskId) {
        const commentHtml = `
            <div class="comment">
                <div class="comment-header">
                    <span class="username">${user}</span>
                    <span class="time">Just now</span>
                </div>
                <div class="comment-body">
                    ${message}
                </div>
            </div>
        `;
        document.getElementById("commentsList").innerHTML += commentHtml;
    }
});

// Kết nối đến SignalR server
connection.start()
    .then(() => console.log("SignalR connected"))
    .catch(err => console.error("SignalR error: ", err));

// Gửi bình luận mới
function sendComment(taskId) {
    const user = "YourUserName"; // TODO: Lấy từ session hoặc backend
    const message = document.getElementById("commentText").value;

    if (message.trim() !== "") {
        connection.invoke("SendMessage", user, message, taskId)
            .then(() => {
                document.getElementById("commentText").value = ""; // Xóa nội dung sau khi gửi
            })
            .catch(err => console.error("Error sending message: ", err));
    } else {
        alert("Comment cannot be empty.");
    }
}

// Thêm sự kiện cho nút Post
document.getElementById("submitComment").addEventListener("click", () => {
    const taskId = document.getElementById("taskModal").getAttribute("data-task-id");
    sendComment(taskId);
});

// Reset modal khi mở
function resetModal(taskId) {
    document.getElementById("commentsList").innerHTML = "";
    document.getElementById("commentText").value = "";
    console.log("task id: " + taskId);
    // Lấy các comment cũ từ API
    fetch(`/api/Comment/GetComments?taskId=${taskId}`)
        .then(response => response.json())
        .then(data => {
            const commentsList = document.getElementById("commentsList");
            data.forEach(comment => {
                const commentHtml = `
                    <div class="comment">
                        <div class="comment-header">
                            <span class="username">${comment.user}</span>
                            <span class="time">${new Date(comment.createdAt).toLocaleString()}</span>
                        </div>
                        <div class="comment-body">
                            ${comment.content}
                        </div>
                    </div>
                `;
                commentsList.innerHTML += commentHtml;
            });
        })
        .catch(err => console.error("Error loading comments: ", err));
}

// Khi click vào một task trên Kanban
function openTaskModal(el) {
    const taskId = el.dataset.eid;
    document.getElementById("taskModal").setAttribute("data-task-id", taskId);
    document.getElementById("taskTitle").innerText = el.innerText;
   
    resetModal(taskId);
    

    const taskModal = new bootstrap.Modal(document.getElementById("taskModal"));
    taskModal.show();
}



