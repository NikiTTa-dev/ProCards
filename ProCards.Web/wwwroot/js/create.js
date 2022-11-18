import { sendData } from './api.js';

const form = document.querySelector('form');
const cards = document.querySelector('.cards');
const addCategoryBtn = form.querySelector('.add');
const addCategoryBtnText = addCategoryBtn.querySelector('span');
const addCategoryInput = addCategoryBtn.querySelector('.add-category');
const bigCards = form.querySelectorAll('.big-card');
const categoryCardTemplate = document.querySelector('#category-card').content.querySelector('li');
const succsess = document.querySelector('.succsess');
const succsessBtn = document.querySelector('#succsess-ok');
const error = document.querySelector('.error');
let errTextContent = error.querySelector('span').textContent
const errorBtn = document.querySelector('#error-ok');

const newCardData = {
    firstSide: '',
    secondSide: '',
    cardCategory: {
        name: '',
        isUserCategory: true
    }
};

function createNewCategory() {
    const newCardCategory = categoryCardTemplate.cloneNode(true);
    const newCardCategoryText = newCardCategory.querySelector('span');
    const input = newCardCategory.querySelector('input');
    input.value = newCardCategoryText.textContent = addCategoryInput.value;
    input.checked = true;
    cards.append(newCardCategory);
}

function showAddCategoryInput() {
    addCategoryBtnText.classList.add('hidden');
    addCategoryInput.classList.remove('hidden');
}

function hideAddCategoryInput() {
    addCategoryBtnText.classList.remove('hidden');
    addCategoryInput.classList.add('hidden');
    addCategoryInput.value = '';
}

function transformToStandartStyle() {
    addCategoryInput.classList.add('hidden');
    addCategoryBtnText.classList.remove('hidden');
    bigCards.forEach((bigCard) => {
        bigCard.querySelector('textarea').value = '';
        bigCard.querySelector('span').classList.remove('hidden');
    })
    errTextContent = '';
}

addCategoryBtn.onclick = () => showAddCategoryInput();

addCategoryInput.onchange = () => {
    createNewCategory();
    hideAddCategoryInput();
}

bigCards.forEach((bigCard) => {
    bigCard.onclick = () => bigCard.querySelector('span').classList.add('hidden');
});

form.onsubmit = (evt) => {
    evt.preventDefault();
    const formData = new FormData(form);
    newCardData.cardCategory.name = formData.get('cardCategory');
    newCardData.firstSide = formData.get('firstSide');
    newCardData.secondSide = formData.get('secondSide');
    let link = 'https://localhost:7141/cards';
    sendData(link, newCardData, onSuccsesPost, onErrorPost);
    transformToStandartStyle();
}

function onSuccsesPost() {
    succsess.classList.remove('hidden');
}

function onErrorPost(err) {
    error.classList.remove('hidden');
    errTextContent = error.querySelector('span').textContent = err;
}

succsessBtn.onclick = () => {
    succsess.classList.add('hidden');
};

errorBtn.onclick = () => {
    error.classList.add('hidden');
};

