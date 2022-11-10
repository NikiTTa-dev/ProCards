import { getData } from "./api.js";

const cardsFromServer = [
    { firstSide: '1(1)', secondSide: '1(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '2(1)', secondSide: '2(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '3(1)', secondSide: '3(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '4(1)', secondSide: '4(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '5(1)', secondSide: '5(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '6(1)', secondSide: '6(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '7(1)', secondSide: '7(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '8(1)', secondSide: '8(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '9(1)', secondSide: '9(2)', cardCategory: { name: 'алгебра' } },
    { firstSide: '10(1)', secondSide: '10(2)', cardCategory: { name: 'алгебра' } },
];

const bigCard = document.querySelector('.big-card');
const rating = document.querySelector('.rating');
const ratingButtons = rating.querySelector('.rating-buttons');
const form = document.querySelector('form');
const scoreList = document.querySelector('.score');
let currentCards = [];
let currentCard = null;


function showNextCard() {
    currentCard = currentCards.shift();
    bigCard.querySelector('.big-card-content').textContent = currentCard.firstSide;
}

function reverseCard() {
    bigCard.querySelector('.big-card-content').textContent = currentCard.secondSide;
}


function getCards(data) {
    currentCards = data;
    showNextCard();
}

function onError(err) {
    console.error(err);
}


function increaseScore(evt) {
    const statusClass = evt.target.classList[1];
    const currentScore = scoreList.querySelector(`.${statusClass}-value`);
    const newValue = Number(currentScore.textContent) + 1;
    currentScore.textContent = newValue;
}

form.onchange = (evt) => {
    if (evt.target.matches('input[type="radio"]')) {
        //Получение массива карточек с сервера
        //const link = `https://localhost:7141/cards?name=${evt.target.value}&isuser=`.concat(
        //evt.target.closest('.user-card') ? 'true' : 'false', '&count=10');
        //getData(link, getCards, onError);

        //Замена получения заккоментировать при работе с сервером
        getCards(cardsFromServer);
    }
};

bigCard.onclick = () => {
    reverseCard();
    rating.classList.remove('hidden');
}

ratingButtons.onclick = (evt) => {
    if (currentCard) {
        increaseScore(evt);
        showNextCard();
    }
    rating.classList.add('hidden');
}
