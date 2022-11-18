import {getCardsFromServer, getCategoriesFromServer, sendData} from './api.js';
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
let rating = document.querySelector('.rating');
const ratingButtons = rating.querySelector('.rating-buttons');
const form = document.querySelector('form');
const scoreList = document.querySelector('.score');
const err = document.querySelector('.error');
let errText = err.querySelector('span');
const errBtn = document.querySelector('#error-ok');
let currentCards = [];
let currentCard = null;
let currentFirstUserCategoryId = 11;
let firstPartCards = [];
let secondPartCards = [];
let isFirstPartCards = true;
let passedCards = [];

function showNextCard() {
    currentCard = currentCards.shift();
    bigCard.querySelector('.big-card-content').textContent = currentCard.firstSide;
}

function reverseCard() {
    bigCard.querySelector('.big-card-content').textContent = currentCard.secondSide;
}

function changeUserCategoriesContent(data) {
    for (let i = 0; i < data.length; i++) {
        const userCard = userCards[i];
        userCard.querySelector('span').textContent = data[i].name;
        userCard.querySelector('input').value = data[i].name;
    }
}

function getUsersCategories(data, isLast) {
    changeUserCategoriesContent(data);
    currentFirstUserCategoryId += data.length;
    if (isLast) {
        currentFirstUserCategoryId = 20;
    }
}

function getCards(data) {
    const length = data.length / 2;
    data.forEach(card => card.grades = [])
    firstPartCards = data.slice(0, length);
    secondPartCards = data.slice(length);
    currentCards = firstPartCards;
    showNextCard();
}

function onErrorGet(error) {
    err.classList.remove('hidden');
    errText.textContent = error;
}


function increaseScore(evt) {
    const statusClass = evt.target.classList[1];
    const currentScore = scoreList.querySelector(`.${statusClass}-value`);
    const newValue = Number(currentScore.textContent) + 1;
    currentScore.textContent = newValue;
}

function addCardToPassedCards(evt) {
    currentCard.grades.push(evt.target.value)
    passedCards.push(currentCard);
}

function getNewCards(data) {
    //Is current cards link to current part of cards?
    if (isFirstPartCards) {
        firstPartCards = data;
        isFirstPartCards = false;
    } else {
        secondPartCards = data;
        isFirstPartCards = true;
    }
    // if (isFirstPartCards)
    //     firstPartCards = data;
    // else
    //     secondPartCards = data;
}

function closeErrorPopup() {
    err.classList.add('hidden');
    errText.textContent = '';
}

form.onchange = (evt) => {
    if (evt.target.matches('input[type="radio"]')) {
        // Get cards from server
        const link = `https://localhost:7141/cards?name=${evt.target.value}&isuser=${evt.target.closest('.user-card') ? true : false}&count=10`;
        getCardsFromServer(link, getCards, onErrorGet);

        //Get cards without server (for testing)
        // getCards(cardsFromServer);
    }
};

refreshBtn.onclick = () => {
    //Get other users categories from server (always 9 pieces)
    getCategoriesFromServer(`https://localhost:7141/categories?firstid=${currentFirstUserCategoryId}`, getUsersCategories, onErrorGet);
    // Get other(same) users categories without server (for testing)
    // getUsersCategories(usersCategotiesFromServer);
};

bigCard.onclick = () => {
    reverseCard();
    rating.classList.remove('hidden');
};

ratingButtons.onclick = (evt) => {
    increaseScore(evt);
    addCardToPassedCards(evt);
    rating.classList.add('hidden');

    console.log(`${firstPartCards.toString()} ${secondPartCards.toString()} ${passedCards.toString()}`)
    
    if (!currentCards.length) {
        let link = 'https://localhost:7141/cards/new';
        // Send passed cards and get new cards from server by algoritm
        if (isFirstPartCards) {
            currentCards = secondPartCards;
        } else {
            currentCards = firstPartCards;
        }
        sendData(link, passedCards, getNewCards, onErrorGet);
        passedCards = [];
        //Change part of cards 
        
    }

    showNextCard();
};

errBtn.onclick = () => {
    closeErrorPopup();
}

getCategoriesFromServer(`https://localhost:7141/categories?firstid=${currentFirstUserCategoryId}`, getUsersCategories, onErrorGet);
// get users categories without server (for testing)
// getUsersCategories(usersCategotiesFromServer);
