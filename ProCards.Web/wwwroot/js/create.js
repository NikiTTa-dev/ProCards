import { getCategoriesFromServer, sendData } from './api.js';

const form = document.querySelector('form');
const cards = document.querySelector('.cards');
const userCategories = cards.querySelectorAll('.user-category');
const addCategoryBtn = form.querySelector('.add');
const addCategoryBtnText = addCategoryBtn.querySelector('span');
const addCategoryInput = addCategoryBtn.querySelector('.add-category');
const bigCards = form.querySelectorAll('.big-card');
const categoryCardTemplate = document.querySelector('#category-card').content.querySelector('li');
const succsess = document.querySelector('.succsess');
const succsessBtn = document.querySelector('#succsess-ok');
const error = document.querySelector('.error');
const errText = error.querySelector('span');
const errorBtn = document.querySelector('#error-ok');
const newCardData = {
    firstSide: '',
    secondSide: '',
    category: {
        name: '',
        isUserCategory: true
    }
};

function changeUserCategoriesContent(data) {
    data.forEach((category, i) => {
        const userCard = userCategories[i];
        userCard.querySelector('span').textContent = category.name;
        userCard.querySelector('input').value = category.name.toLowerCase();
    })
}
getCategoriesFromServer(`https://localhost:7141/categories?firstid=11`,
    changeUserCategoriesContent, showErrorOnGet);

function showAddCategoryInput() {
    addCategoryBtnText.classList.add('hidden');
    addCategoryInput.classList.remove('hidden');
}

function hideAddCategoryInput() {
    addCategoryBtnText.classList.remove('hidden');
    addCategoryInput.classList.add('hidden');
    addCategoryInput.value = '';
}

function resetForm() {
    addCategoryInput.classList.add('hidden');
    addCategoryBtnText.classList.remove('hidden');
    bigCards.forEach(bigCard => {
        bigCard.querySelector('textarea').value = '';
        bigCard.querySelector('span').classList.remove('hidden');
    })
    errText.textContent = '';
}

function addNewCategory(newCardCategory) {
    cards.insertBefore(newCardCategory, cards.firstElementChild.nextSibling);
    cards.removeChild(cards.lastElementChild);
}

function createNewCategory() {
    const newCardCategory = categoryCardTemplate.cloneNode(true);
    const newCardCategoryText = newCardCategory.querySelector('span');
    const input = newCardCategory.querySelector('input');
    newCardCategoryText.textContent = addCategoryInput.value;
    input.value = addCategoryInput.value.toLowerCase();
    input.checked = true;
    addNewCategory(newCardCategory);
    hideAddCategoryInput();
}

addCategoryBtn.onclick = showAddCategoryInput;
addCategoryInput.onchange = createNewCategory;

bigCards.forEach(bigCard => bigCard.onclick = () => bigCard.querySelector('span').classList.add('hidden'));

function onFormIgnoreSubmitOnEnter(evt) {
    if (evt.key === 'Enter') {
        evt.preventDefault();
    }
}

form.onkeydown = onFormIgnoreSubmitOnEnter;

form.onsubmit = evt => {
    evt.preventDefault();
    const formData = new FormData(form);
    newCardData.category.name = formData.get('cardCategory');
    newCardData.firstSide = formData.get('firstSide');
    newCardData.secondSide = formData.get('secondSide');
    sendData('https://localhost:7141/cards', newCardData, showSuccsessPopup, showErrorOnSend);
    resetForm();
}

function showSuccsessPopup() {
    succsess.classList.remove('hidden');
}

function showErrorOnSend(err) {
    error.classList.remove('hidden');
    error.querySelector('p').textContent = 'Не удалось добавить карточку.';
    errText.textContent = err;
}

function showErrorOnGet(err) {
    error.classList.remove('hidden');
    error.querySelector('p').textContent = 'Не удалось получить категории пользователей.';
    errText.textContent = err;
}

succsessBtn.onclick = () => succsess.classList.add('hidden');
errorBtn.onclick = () => error.classList.add('hidden');

