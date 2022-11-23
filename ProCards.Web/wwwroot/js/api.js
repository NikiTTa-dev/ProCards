function getCardsFromServer(link, onSuccsess, onError) {
    fetch(link)
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error(`${response.status} ${response.statusText}`);
        })
        .then(data => onSuccsess(data))
        .catch(err => onError(err));
}

function getCategoriesFromServer(link, onSuccsess, onError) {
    let isLast = false;

    fetch(link)
        .then(response => {
            if (response.ok) {
                if (response.headers.get("is-last") === 'true') {
                    isLast = true;
                }
                return response.json();
            }
            throw new Error(`${response.status} ${response.statusText}`);
        })
        .then((data) => {
            onSuccsess(data, isLast);
            isLast = false;
        })
        .catch(err => onError(err));
}

function sendData(link, newCardData, onSuccsess, onError) {
    let isFail = false;

    fetch(
        link,
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newCardData)
        })
        .then(response => {
            if (response.ok) {
                if (response.headers.get("content-length"))
                    return response.json();
                else return null;
            } else {
                isFail = true;
                return response.text();
            }
        })
        .then(data => {
            if (isFail) {
                throw new Error(`${data}`);
            }
            else {
                onSuccsess(data);
            }
        })
        .catch(err => onError(err));
}

export { getCardsFromServer, getCategoriesFromServer, sendData };