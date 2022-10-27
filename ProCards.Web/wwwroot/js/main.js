let page = document.querySelector('.page');

function switchTheme() {
    let themeDarkButton = document.querySelector('.theme-button-dark');
    let themeLightButton = document.querySelector('.theme-button-light');

    themeDarkButton.onclick = function () {
        page.classList.toggle('dark');
        this.classList.add('active');
        themeLightButton.classList.remove('active');
    }

    themeLightButton.onclick = function () {
        page.classList.toggle('dark');
        this.classList.add('active');
        themeDarkButton.classList.remove('active');
    }
}

function switchFontStyle() {
    let fontButtonSans = document.querySelector('.font-button-sans-serif');
    let fontButtonSerif = document.querySelector('.font-button-serif');

    fontButtonSans.onclick = function () {
        page.classList.toggle('serif');
        this.classList.add('active');
        fontButtonSerif.classList.remove('active');
    }

    fontButtonSerif.onclick = function () {
        page.classList.toggle('serif');
        this.classList.add('active');
        fontButtonSans.classList.remove('active');
    }
}

switchTheme();
switchFontStyle();

function readMore() {
    let articles = document.querySelectorAll('.blog-article.short');
    for (let article of articles) {
        let moreButton = article.querySelector('.more');
        moreButton.onclick = function () {
            article.classList.remove('short');
        }
    }
}

readMore();

function changeCardsStyle() {
    let cards = document.querySelector('.cards');
    let cardButtonGrid = document.querySelector('.card-view-button-grid');
    let cardButtonList = document.querySelector('.card-view-button-list');

    cardButtonGrid.onclick = function () {
        cards.classList.toggle('list');
        this.classList.add('active');
        cardButtonList.classList.remove('active');
    }

    cardButtonList.onclick = function () {
        cards.classList.toggle('list');
        this.classList.add('active');
        cardButtonGrid.classList.remove('active');
    }
}

changeCardsStyle();

function changeActivePhoto() {
    let activePhoto = document.querySelector('.active-photo');
    let previewList = document.querySelector('.preview-list');
    let previews = previewList.querySelectorAll('a');
    for (const preview of previews) {
        preview.onclick = function (evt) {
            evt.preventDefault();
            activePhoto.src = preview.href;
            let currentActivePreview = previewList.querySelector('.active-item');
            currentActivePreview.classList.remove('active-item');
            this.classList.add('active-item');
        }
    }
}

changeActivePhoto();
