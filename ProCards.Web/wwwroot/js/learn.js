const bigCard = document.querySelector('.big-card');
const rating = document.querySelector('.rating');
const ratingButtons = rating.querySelector('.rating-buttons');
const form = document.querySelector('form');

const newCardData = {
    firstSide: 'abc',
    secondSide: 'cba',
    cardCategory: {
        name: 'algebra'
    }
}

let currentCards = [];
let currentCard = null;

function showNextCard() {
    currentCard = currentCards.shift();
    bigCard.textContent = currentCard.firstSide;
}

function reverseCard() {
    bigCard.textContent = currentCard.secondSide;
}

form.onchange = (evt) => {
    if (evt.target.matches('input[type="radio"]')) {
        const link = `https://localhost:7141/cards?name=${evt.target.value}&isuser=true`;
        fetch(link).then(response => 
            response.json()).then(cards => {
            currentCards = cards;
            showNextCard();
        });
            //console.log();
            //showNextCard();
    }
};

bigCard.onclick = () => {
    reverseCard();
    rating.classList.remove('hidden');
}

ratingButtons.onclick = () => {
    showNextCard();
    rating.classList.add('hidden');
}