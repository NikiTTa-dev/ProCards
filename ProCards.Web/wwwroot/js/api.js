function getData(link, onSucsess, onError) {
    fetch(link)
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error(`${response.status} ${response.statusText}`);
        })
        .then(data => {
            onSucsess(data);
        })
        .catch((err) => {
            onError(err);
        });
}

function sendData(newCardData) {
    fetch(
        'https://localhost:7141/cards',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newCardData)
        });
}

export { getData, sendData }