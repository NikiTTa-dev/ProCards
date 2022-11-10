import { sendData } from './api.js';

const form = document.querySelector('form');
const cards = document.querySelector('.cards');
const addCategoryButton = form.querySelector('.add');
const addCategoryButtonText = addCategoryButton.querySelector('span');
const addCategoryInput = addCategoryButton.querySelector('.add-category');
const bigCards = form.querySelectorAll('.big-card');
const categoryCardTemplate = document.querySelector('#category-card').content.querySelector('li');

const newCardData = {
    firstSide: '',
    secondSide: '',
    cardCategory: {
        name: ''
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
    addCategoryButtonText.classList.add('hidden');
    addCategoryInput.classList.remove('hidden');
}

function hideAddCategoryInput() {
    addCategoryButtonText.classList.remove('hidden');
    addCategoryInput.classList.add('hidden');
    addCategoryInput.value = '';
}

function transformToStandartStyle() {
    addCategoryInput.classList.add('hidden');
    addCategoryButtonText.classList.remove('hidden');
    bigCards.forEach((bigCard) => {
        bigCard.querySelector('textarea').value = '';
        bigCard.querySelector('span').classList.remove('hidden');
    })
}

addCategoryButton.onclick = () => showAddCategoryInput();

addCategoryInput.onchange = () => {
    createNewCategory();
    hideAddCategoryInput()
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
    sendData(newCardData);
    transformToStandartStyle();
}
