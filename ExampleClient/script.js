//A variable with our URL
const URL = "http://localhost:8000/";

// A function to fetch our array of tasks (our data)
const getData = () => {
  fetch(URL)
    .then((response) => response.json()) //like we did for API challenge
    .then((data) => {
      console.log(data);
      data.forEach((task) => appendItem(task));
    });
};

const appendItem = (task) => {
  let container = document.getElementById("task-container");
  let div = document.createElement("div");
  let header = document.createElement("h3");
  let p = document.createElement("p");

  div.id = task.Id;
  div.className = "task"; //setting class attribute to div that is appended to container

  header.innerText = task.Title;

  p.innerText = task.Description;

  div.appendChild(header);
  div.appendChild(p);

  if (task.IsComplete) {
    div.classList.add("completed");
    moveTask(div);
  } else {
    div.onclick = () => completeTask(task.Id); //if click on this div will be marked as complete
    container.appendChild(div);
  }
};

const handleSubmit = (event) => {
  event.preventDefault(); //stops webpage from appending to page and reloading
  console.log(event);

  // Take the HTMLFormElement and turn it into FormData
  const formData = new FormData(event.target); //digging into array
  // Convert FormData into a simple object
  const obj = Object.fromEntries(formData);
  console.log(obj); //{title: 'Title', description: 'Description'}

  //communicate from client to server
  fetch(URL, {
    method: "POST",
    mode: "cors", //cross origin
    body: JSON.stringify(obj), //returns a true or false whether or not a body involved
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error(response.status);
      }
      return response.json();
    }) //gives what the webserver responds with
    .then((data) => {
      console.log(data);
      appendItem(data); //appends the task without needing to refresh
    })
    .catch((err) => console.error(err));

  event.target.reset(); //reset the form so can't just keep hitting add new task
};

const completeTask = (taskId) => {
  const div = document.getElementById(taskId);

  fetch(URL, {
    method: "PUT",
    mode: "cors",
    body: JSON.stringify({ taskId }),
  }).then((response) => {
    if (response.ok) {
      div.classList.add("completed");
      moveTask(div); //passing div to moveTask element
    }
  });
};

const moveTask = (div) => {
  const newParent = document.getElementById("completed-tasks-container");
  newParent.appendChild(div); //appending to the completed tasks container
  div.onclick = () => deleteTask(div); //next time you click it, it will be deleted
};

const deleteTask = (taskEl) => {
  const deleteUrl = `${URL}${taskEl.id}`; //giving us the id from div taskEl and placing on end of url
  console.log(deleteUrl);

  //sending a delete request to server
  fetch(deleteUrl, {
    method: "DELETE",
    mode: "cors",
  }).then((res) => {
    console.log(res);
    if (res.ok) {
      taskEl.remove(); //telling taskEl to be removed from the page
    }
  });
};
