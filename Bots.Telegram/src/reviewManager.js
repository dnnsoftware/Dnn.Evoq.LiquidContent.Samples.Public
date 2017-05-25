import request from 'request';
import config from './config';
import reviewService from './reviewService';

let _reviews = [];
let _totalReviews = 0;

const reviewManager = {
    setReviews(data) {
        _reviews = data.documents;
        _totalReviews = data.totalResultCount;
    },
    getReviews() {
        return _reviews;
    },
    getReview(index) {
        return _reviews[index - 1];
    },
    getTotalReviews() {
        return _totalReviews;
    },
    printReviews() {
        let result = _totalReviews + ' Reviews\n';

        _reviews.forEach(
            (review, i) => {
                const { name, comment } = review.details;
                const index = i + 1;
                result += index + ') ' + name + ': ' + comment + '\n';
            }
        );
        return result;
    }
};

export default reviewManager;
