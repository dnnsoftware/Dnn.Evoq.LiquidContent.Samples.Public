const request = require('request');

const apiKey = 'ec50a1f261e14337b719b42db5c6804d';
const endPointUrl = 'https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Description,Tags';

function describeImage(imageUrl) {
    const payload = {
        url: imageUrl
    }    
    return new Promise((resolve, reject) => {        
        const options = {
            method: 'POST',
            url: endPointUrl,
            body: JSON.stringify(payload),
            headers: {
                'Content-type': 'application/json',
                'Ocp-Apim-Subscription-Key': apiKey
            }
        };
        request(options, function(error, response, body){
            if(error) {
                reject(error);
                return;
            }
            const jsonBody = JSON.parse(body);
            resolve(jsonBody);
        });
    });
}

const computerVisionService = {
    describeImage
};
module.exports = computerVisionService;