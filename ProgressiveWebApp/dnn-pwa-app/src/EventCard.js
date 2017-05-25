import React from 'react';
import {Card, CardActions, CardHeader, CardMedia, CardTitle, CardText} from 'material-ui/Card';
import Avatar from 'material-ui/Avatar';
import PropTypes from 'prop-types';

function getAvatar(avatarUrl){
    return <Avatar src={avatarUrl} />;
}

function formatDate(date) {
    const dateOptions = { year: "numeric", month: "short", day: "2-digit", hour: "2-digit", minute: "2-digit"};
    return new Date(date).toLocaleDateString("en-US", dateOptions);
}

const EventCard = (props) => (
    <Card style={{margin: 15}}>
        <CardHeader
            title={props.publicationUser}
            subtitle={formatDate(props.publicationTime)}
            avatar={getAvatar(props.publicationAvatar)}
        />
        <CardTitle title={props.title} subtitle={props.subtitle} />
    </Card>
);

EventCard.propTypes = {
    publicationUser: PropTypes.string,
    publicationTime: PropTypes.string,
    publicationAvatar: PropTypes.string,
    title: PropTypes.string,
    subtitle: PropTypes.string
};

export default EventCard;