let isLoggedIn;

let header = `<nav class="header navbar navbar-expand-lg navbar-light px-2">
      <a class="title-container" href="@Url.Action("Index", "Home")">
        <img class="header-image" src="~/static/logo.png" />
        <p class="header-title"><span style="color: #984fca;">Travel<span/> <span style="color: #fe9046">GO<span/></p>
      </a>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Index", "Home")">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Register", "Account")">Register</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Contact", "Home")">Contact Us</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Index", "Packages")">Packages</a>
          </li>
        </ul>
      </div>
      <section class="action-buttons user-info-header">
        <button type="button" class="btn btn-light mx-2" onclick="window.location.href = '@Url.Action("Login", "Account")';">Login</button>
        <button type="button" class="btn btn-secondary mr-2" onclick="location.href = '@Url.Action("Register", "Account")'">Sign up</button>
      </section>
    </nav>`;

const setHeader = ({ CustFirstName, CustLastName }) => {
    const newHeader = `<nav class="header navbar navbar-expand-lg navbar-light px-2">
      <a class="title-container" href="@Url.Action("Index", "Home")">
        <img class="header-image" src="~/static/logo.png" />
      </a>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Index", "Home")">Home</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Register", "Account")">Register</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Contact", "Home")">Contact Us</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="@Url.Action("Index", "Packages")">Packages</a>
          </li>
        </ul>
      </div>
      <section class="user-info-header">
        Welcome, ${[CustFirstName, CustLastName].join(" ")}
        <button type="button" class="btn btn-secondary mx-2" onclick="window.location.href = '@Url.Action("Logout", "Account")';">Logout</button>
        <button type="button" class="btn mx-2" onclick="window.location.href = '@Url.Action("Dashboard", "Account")';">Profile</button>
      </section