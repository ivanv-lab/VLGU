function countMaxScore(elements) {
    var tmpScore = 0;
    for (i = 0; i < elements.length; i++) {
        if (elements[i].hasAttribute('data-true'))
            tmpScore += parseInt(elements[i].getAttribute('data-true'));
    }
    return tmpScore;
}

function radioCheckAnswer(answers) {
    var tmpScore = 0;
    for (var i = 0; i < answers.length; i++) {
        if (answers[i].hasAttribute('data-true') &&
            answers[i].checked) {
            tmpScore +=
                parseInt(answers[i].getAttribute('data-true'));
        }
    }
    return tmpScore;
}

function textAnswer(answer) {
    var tmpScore = 0;
    var correct = answer.getAttribute('data-ans').toLowerCase();
    if (answer.value.toLowerCase() == correct) {
        tmpScore += parseInt(answer.getAttribute('data-true'));
    }
    return tmpScore;
}

function score() {
    var test = document.getElementById('test');
    var elements = test.getElementsByTagName('input');
    var maxScore = countMaxScore(elements);
    var userScore = 0;
    var queName;
    var answers;
    var i = 0;

    while (i < elements.length) {
        queName = elements[i].getAttribute('name');
        answers = [];
        while (i < elements.length && elements[i].getAttribute('name') == queName) {
            answers[answers.length] = elements[i];
            i++;
        }
        if (answers.length > 0) {
            if (answers[0].getAttribute('type') == 'radio' ||
                answers[0].getAttribute('type') == 'checkbox') {
                userScore += radioCheckAnswer(answers);
            } else if (answers[0].getAttribute('type') == 'text') {
                userScore += textAnswer(answers[0]);
            }
        }
    }
    alert("Your score: " + userScore + "\nMax score: " + maxScore);
}