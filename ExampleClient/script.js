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

  container.appendChild(div);
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
