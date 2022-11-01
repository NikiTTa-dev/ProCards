const submitButton = document.querySelector('.submit');

submitButton.onclick = () => {
    const newCardData = {
        firstSide: 'abc',
        secondSide: 'cba',
        cardCategory: {
            name: 'algebra'
        }
    }

    fetch(
        'https://localhost:7141/cards',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(newCardData)
        });
}