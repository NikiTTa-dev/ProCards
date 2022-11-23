const body = document.body;
const themeSwitchBtn = document.querySelector('.theme-switch-button');
const signin = document.querySelector('.signin');
const signinImg = signin.querySelector('img');

function darkmode() {
  const wasDarkmode = localStorage.getItem('darkmode') === 'true';
  localStorage.setItem('darkmode', !wasDarkmode);
  body.classList.toggle('dark', !wasDarkmode);

  localStorage.setItem('themeUrl', !wasDarkmode ? "url('../content/sun-light.svg')" : "url('../content/moon.svg')");
  this.style.backgroundImage = localStorage.getItem('themeUrl');
  this.classList.toggle('selected', !wasDarkmode);

  localStorage.setItem('signinSrc', !wasDarkmode ? "../content/user-light.svg" : "../content/user.svg");
  signinImg.src = localStorage.getItem('signinSrc');
  signin.classList.toggle('selected', !wasDarkmode);
}

themeSwitchBtn.onclick = darkmode;

function onLoad() {
  const wasDarkmode = localStorage.getItem('darkmode') === 'true';
  body.classList.toggle('dark', wasDarkmode);
  themeSwitchBtn.style.backgroundImage = localStorage.getItem('themeUrl');
  themeSwitchBtn.classList.toggle('selected', wasDarkmode);
  signinImg.src = localStorage.getItem('signinSrc') ?? "../content/user.svg";
  signin.classList.toggle('selected', wasDarkmode);
}
document.addEventListener('DOMContentLoaded', onLoad);