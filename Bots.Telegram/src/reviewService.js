import request from 'request';
import config from './config';

const apiEndPointUrl = 'https://dnnapi.com/content/api/ContentItems/';

const reviewService = {
    getDraftReviews() {
        return new Promise((resolve, reject) => {
            const options = {
                method: 'GET',
                url: apiEndPointUrl,
                qs: {
                    startIndex: 0,
                    maxItems: 5,
                    fieldOrder: 'createdAt',
                    orderAsc: false,
                    contentTypeId: '4fc401e9-c4fa-4c49-8b29-aa0e02d7e856',
                    published: false
                },
                headers: {
                    'Content-type': 'application/json',
                    'Authorization': 'Bearer ' + config.apiKey
                }
            };
            request(options, function(error, response, body){
                if (error) {
                    reject(error);
                }
                else {
                    resolve(JSON.parse(response.body));
                }
            });
        });
    },
    publishReview(review) {
        return new Promise((resolve, reject) => {
            const options = {
                method: 'PUT',
                url: apiEndPointUrl + review.id,
                body: JSON.stringify(review),
                qs: {
                    publish: true
                },
                headers: {
                    'Content-type': 'application/json',
                    'Authorization': 'Bearer ' + config.apiKey
                }
            };
            request(options, function(error, response, body){
                if (error) {
                    reject(error);
                }
                else {
                    resolve(JSON.parse(response.body));
                }
            });
        });
    }
};
export default reviewService;
