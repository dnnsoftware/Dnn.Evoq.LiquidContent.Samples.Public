const eventEndpoint = window.backendEndPoint + "dnnGetEvents";

const eventService = {
    getEvents() {
        return fetch(eventEndpoint, {
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                    method: 'GET'
            }).then(response => response.json());
    }
}

export default eventService;