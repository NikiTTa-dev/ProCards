const form = document.querySelector('form');
const addCategoryButton = form.querySelector('.add');
const addRadio = form.querySelector('#add-radio');
const addCategoryButtonText = addCategoryButton.querySelector('span');
const addCategoryInput = addCategoryButton.querySelector('.add-category');
const bigCards = form.querySelectorAll('.big-card');

addCategoryButton.onclick = () => {
    addCategoryButtonText.textContent = '';
    addRadio.classList.add('hidden');
    addCategoryInput.classList.remove('hidden');
}

addCategoryInput.onchange = () => {
    addRadio.value = addCategoryInput.value;
}

bigCards.forEach(bigCard => {
    bigCard.onfocus = () => {
        bigCard.querySelector('span').textContent = '';
        bigCard.querySelector('textarea').style.display = 'block';
    }
});

form.onsubmit = (evt) => {
    evt.preventDefault();
    alert('Карточка создана!');
    addCategoryInput.classList.add('hidden');
    bigCards[0].querySelector('span').textContent = 'Лицевая сторона';
    bigCards[1].querySelector('span').textContent = 'Обратная сторона';
    bigCards[0].querySelector('textarea').style.display = 'none';
    bigCards[1].querySelector('textarea').style.display = 'none';
    addRadio.checked = false;
    addCategoryButtonText.textContent = '+';
}