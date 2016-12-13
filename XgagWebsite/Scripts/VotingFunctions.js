function vote(postId, type) {
    var isUpvote = type > 0;
    var button = $('#' + (isUpvote ? 'upvote-' : 'downvote-') + postId);
    if (button.hasClass('disabled')) {
        return;
    }

    var twinButton = $('#' + (!isUpvote ? 'upvote-' : 'downvote-') + postId);
    var isUnvote = button.attr('data-unvote').toLowerCase() == 'true';
    var voteUrl = isUnvote ? '/Posts/Unvote' : '/Posts/Vote';
    var data = {
        postId: postId,
        voteType: type
    };

    $.post(voteUrl, data);

    if (!isUnvote) {
        button.removeClass('btn-primary');
        if (isUpvote) {
            button.addClass('btn-success');
            twinButton.addClass('disabled');
        } else {
            button.addClass('btn-danger');
            twinButton.addClass('disabled');
        }
        button.attr('data-unvote', 'True');

    } else {
        if (isUpvote) {
            button.removeClass('btn-success');
            twinButton.removeClass('disabled');
        } else {
            button.removeClass('btn-danger');
            twinButton.removeClass('disabled');
        }
        button.addClass('btn-primary');
        button.attr('data-unvote', 'False');
        twinButton.attr('data-unvote', 'False');
    }

    correctScore(postId, isUpvote, isUnvote);
}

function correctScore(postId, isUpvote, isUnvote) {
    var scoreElement = $('#score-' + postId);
    var currentScore = parseInt(scoreElement.text());
    var corrector = isUpvote ? 1 : -1;
    if (isUnvote) {
        corrector *= -1;
    }
    scoreElement.text(currentScore + corrector);
}