const threshold = 10;
let seconds = 0;
let clicks = 0;
const currentScoreElement = document.getElementById("current_score");
const recordScoreElement = document.getElementById("record_score");
const profitPerClickElement = document.getElementById("profit_per_click");
const profitPerSecondElement = document.getElementById("profit_per_second");
const currentScore = Number(currentScoreElement.innerText);
const recordScore = Number(recordScoreElement.innerText);
const profitPerSecond = Number(profitPerSecondElement.innerText);
const profitPerClick = Number(profitPerClickElement.innerText);


$(document).ready(function () {
    var clickitem = document.getElementById("clickitem");

    clickitem.onclick = click;
    setInterval(addSecond, 1000)
})

function addSecond() {
    seconds++;

    if (seconds >= threshold) {
        addPointsToScore();
    }

    if (seconds > 0) {
        updateUiScore();
    }
}

function click() {
    clicks++;

    if (clicks >= threshold) {
        addPointsToScore();
    }

    if (clicks > 0) {
        updateUiScore();
    }
}

function updateUiScore() {
    var profit = clicks * profitPerClick + seconds * profitPerSecond

    const uiCurrentScore = currentScore + profit;
    const uiRecordScore = recordScore + profit;

    currentScoreElement.innerText = uiCurrentScore;
    recordScoreElement.innerText = uiRecordScore;
}

function addPointsToScore() {
    $.ajax({
        url: '/score',
        method: 'post',
        dataType: 'json',
        data: { clicks: clicks, seconds: seconds },
        success: onAddPointsSuccess(),
    });
}

function onAddPointsSuccess() {
    seconds = 0;
    clicks = 0;

    location.reload();
}