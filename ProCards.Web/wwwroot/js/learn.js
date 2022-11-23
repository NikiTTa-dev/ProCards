import { getCardsFromServer, getCategoriesFromServer, sendData } from './api.js';
// const cardsFromServer = [
//     { firstSide: '1(1)', secondSide: '1(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '2(1)', secondSide: '2(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '3(1)', secondSide: '3(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '4(1)', secondSide: '4(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '5(1)', secondSide: '5(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '6(1)', secondSide: '6(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '7(1)', secondSide: '7(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '8(1)', secondSide: '8(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '9(1)', secondSide: '9(2)', cardCategory: { name: 'алгебра' } },
//     { firstSide: '10(1)', secondSide: '10(2)', cardCategory: { name: 'алгебра' } },
// ];

// const usersCategotiesFromServer = [
//     { name: 'Информатика', isUserCategory: true },
//     { name: 'Литература', isUserCategory: true },
//     { name: 'История', isUserCategory: true },
//     { name: 'Химия', isUserCategory: true },
//     { name: 'Биология', isUserCategory: true },
//     { name: 'ОБЖ', isUserCategory: true },
//     { name: 'Геометрия', isUserCategory: true },
//     { name: 'Алгебра', isUserCategory: true },
//     { name: 'География', isUserCategory: true },
// ];
const userCards = document.querySelectorAll('.user-card');
const refreshBtn = document.querySelector('.refresh-button');
const bigCard = document.querySelector('.big-card');
const bigCardContent = bigCard.querySelector('.big-card-content');
let rating = document.querySelector('.rating');
const ratingBtns = rating.querySelector('.rating-buttons');
const userScores = document.querySelector('.score').querySelectorAll('span');
const categoriesSelection = document.querySelector('form');
const scoreList = document.querySelector('.score');
const err = document.querySelector('.error');
const errText = err.querySelector('span');
const errBtn = document.querySelector('#error-ok');
let currentCards = [];
let currentCard = null;
let currentFirstUserCategoryId = 11;
let firstPartCards = [];
let secondPartCards = [];
let isFirstPartCards = true;
let passedCards = [];
let isEnoughCards = false;

function showNextCard() {
    currentCard = currentCards.shift();
    bigCardContent.textContent = currentCard.firstSide;
}

function reverseCard() {
    bigCardContent.textContent = currentCard.secondSide;
    rating.classList.remove('hidden');
}

function changeUserCategoriesContent(data) {
    data.forEach((category, i) => {
        const userCard = userCards[i];
        userCard.querySelector('span').textContent = category.name;
        userCard.querySelector('input').value = category.name.toLowerCase();
    })
}

function setUsersCategories(data, isLast) {
    changeUserCategoriesContent(data);
    currentFirstUserCategoryId += data.length;
    if (isLast) {
        currentFirstUserCategoryId = 20;
    }
}

function setCards(data) {
    isEnoughCards = data.length > 9;
    if (!isEnoughCards) {
        currentCards = data;
    } else {
        const length = data.length / 2;
        firstPartCards = data.slice(0, length);
        secondPartCards = data.slice(length);
        currentCards = firstPartCards;
    }
    showNextCard();
}

function onErrorGet(error) {
    err.classList.remove('hidden');
    errText.textContent = error;
}

function increaseUserScore(evt) {
    const currentScore = scoreList.querySelector(`.${evt.target.classList[1]}-value`);
    currentScore.textContent = Number(currentScore.textContent) + 1;
    rating.classList.add('hidden');
}

function clearUserScores() {
    userScores.forEach(score => score.textContent = 0)
}

function addCardToPassedCards(evt) {
    currentCard.grades.push(evt.target.value)
    passedCards.push(currentCard);
}

function setNewCards(data) {
    if (isFirstPartCards) {
        firstPartCards = data;
        isFirstPartCards = false;
    } else {
        secondPartCards = data;
        isFirstPartCards = true;
    }
}

function closeErrorPopup() {
    err.classList.add('hidden');
    errText.textContent = '';
}

function doIntervalRepeatAlgorithm(evt) {
    if (isEnoughCards) {
        addCardToPassedCards(evt);
        if (!currentCards.length) {
            if (isFirstPartCards) {
                currentCards = secondPartCards;
            } else {
                currentCards = firstPartCards;
            }
            sendData('https://localhost:7141/cards/new', passedCards, setNewCards, onErrorGet);
            passedCards = [];
        }
    } else {
        currentCards.push(currentCard);
    }
}

getCategoriesFromServer(`https://localhost:7141/categories?firstid=${currentFirstUserCategoryId}`,
    setUsersCategories, onErrorGet);
// get users categories without server (for testing)
// getUsersCategories(usersCategotiesFromServer);

categoriesSelection.onchange = (evt) => {
    if (evt.target.matches('input[type="radio"]')) {
        clearUserScores();
        getCardsFromServer(`https://localhost:7141/cards?name=${evt.target.value}&isuser=${evt.target.closest('.user-card')
            ? true : false}&count=10`, setCards, onErrorGet);

        //Get cards without server (for testing)
        // getCards(cardsFromServer);
    }
};

refreshBtn.onclick = () => getCategoriesFromServer(`https://localhost:7141/categories?firstid=${currentFirstUserCategoryId}`,
    setUsersCategories, onErrorGet);
// Get other(same) users categories without server (for testing)
// getUsersCategories(usersCategotiesFromServer);

bigCard.onclick = reverseCard;

ratingBtns.onclick = (evt) => {
    increaseUserScore(evt);
    doIntervalRepeatAlgorithm(evt);
    showNextCard();
};

errBtn.onclick = closeErrorPopup;