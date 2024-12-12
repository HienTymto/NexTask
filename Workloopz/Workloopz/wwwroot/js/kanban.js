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
                taskModal.show();
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

