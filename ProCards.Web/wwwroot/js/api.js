let isLast = false;


function getCardsFromServer(link, onSucsess, onError) {
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
        .catch(err => {
            onError(err);
        });
}

function getCategoriesFromServer(link, onSucsess, onError) {
    fetch(link)
        .then(response => {
            if (response.ok) {
                console.log(typeof(isLast));
                if (response.headers.get('is-last') == "true")
                    isLast = true;
                    
                return response.json();
                //response.headers.get('is-last')
            }
            throw new Error(`${response.status} ${response.statusText}`);
        })
        .then((data) => {
            onSucsess(data, isLast);
            isLast = false;
        })
        .catch(err => {
            onError(err);
        });
}

function sendData(link, newCardData, onSucsess, onError) {
    fetch(
        link,
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(newCardData)
        })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error(`${response.status} ${response.statusText}`);
            }
        })
        .then(data=>{
            onSucsess(data);
        })
        .catch(err => {
            onError(err);
        });
}

export { getCardsFromServer, getCategoriesFromServer, sendData };