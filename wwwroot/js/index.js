const threshold = 10;
let seconds = 0;
let clicks = 0;

const currentScoreElement = document.getElementById("current_score");
const recordScoreElement = document.getElementById("record_score");
const profitPerClickElement = document.getElementById("profit_per_click");
const profitPerSecondElement = document.getElementById("profit_per_second");
let currentScore = Number(currentScoreElement.innerText);
let recordScore = Number(recordScoreElement.innerText);
let profitPerSecond = Number(profitPerSecondElement.innerText);
let profitPerClick = Number(profitPerClickElement.innerText);


$(document).ready(function () {
    const clickitem = document.getElementById("clickitem");

    clickitem.onclick = click;
    setInterval(addSecond, 1000)

    const boostButtons = document.getElementsByClassName("boost_button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        boostButton.onclick = () =>
            clickOnBoostButton(boostButton);
    }
})

function addSecond() {
    seconds++;

    if (seconds >= threshold) {
        addPointsToScore();
    }

    if (seconds > 0) {
        addScoreFromSecond();
    }
}

function clickOnBoostButton(button) {
    const boostIdField = button.getElementsByClassName("boost_id")[0];
    const boostIdValue = boostIdField.innerText;

    if (clicks > 0 || seconds > 0) {
        addPointsToScore();
    }

    buyBoost(boostIdValue);
}

function click() {
    clicks++;

    if (clicks >= threshold) {
        addPointsToScore();
    }

    if (clicks > 0) {
        addScoreFromClick();
    }
}

function addScoreFromSecond() {
    currentScore += profitPerSecond;
    recordScore += profitPerSecond;

    updateUiScore();
}

function addScoreFromClick() {
    currentScore += profitPerClick;
    recordScore += profitPerClick;

    updateUiScore();
}

function addUiScore() {
    const profit = clicks * profitPerClick + seconds * profitPerSecond

    currentScore += profit;
    recordScore += profit;

    updateUiScore();

    currentScoreElement.innerText = uiCurrentScore;
    recordScoreElement.innerText = uiRecordScore;
}

function buyBoost(boostId) {
    $.ajax({
        url: '/boost/buy',
        method: 'post',
        dataType: 'json',
        data: { boostId: boostId },
        success: onScoreUpdate,
    });
}

function addPointsToScore() {
    $.ajax({
        url: '/score/add',
        method: 'post',
        dataType: 'json',
        data: { clicks: clicks, seconds: seconds },
        success: onScoreUpdate,
    });
}

function updateUiScore() {
    currentScoreElement.innerText = currentScore;
    recordScoreElement.innerText = recordScore;
    profitPerClickElement.innerText = profitPerClick;
    profitPerSecondElement.innerText = profitPerSecond;

    ToggleBoostsAvailability();
}

function ToggleBoostsAvailability() {
    const boostButtons = document.getElementsByClassName("boost_button");

    for (let i = 0; i < boostButtons.length; i++) {
        const boostButton = boostButtons[i];

        const boostPriceElement = boostButton.getElementsByClassName("boost_price")[0];
        const boostPrice = Number(boostPriceElement.innerText)

        if (boostPrice > currentScore) {
            boostButton.disabled = true;
            continue;
        }

        boostButton.disabled = false;
    }
}

function onScoreUpdate(data) {
    seconds = 0;
    clicks = 0;

    currentScore = Number(data["currentScore"]);
    recordScore = Number(data["recordScore"]);
    profitPerClick = Number(data["profitPerClick"]);
    profitPerSecond = Number(data["profitPerSecond"]);

    updateUiScore();
}