// My Stuff Toggle
const myStuffToggle = document.getElementById("my-stuff-toggle");
const myStuffMenu = document.getElementById("my-stuff-menu");

if (myStuffToggle != undefined && myStuffMenu != undefined) {
    myStuffToggle.addEventListener("click", () => {
        const menuHidden = myStuffMenu.classList.contains("hidden-menu");

        if (menuHidden) myStuffMenu.classList.remove("hidden-menu");
        else myStuffMenu.classList.add("hidden-menu");
    });
}

// Main Menu Toggle(s)
const menuToggles = document.getElementsByClassName("menu-toggle");
for (let i = 0; i < menuToggles.length; i++) {
    const menuToggle = menuToggles[i];
    const menuId = menuToggle.dataset.toggleFor;

    const menu = document.getElementById(menuId);

    menuToggle.addEventListener("click", () => {
        let menuHidden = menu.classList.contains("hidden-menu-md");
        console.log({ classList: menu.classList });

        console.log({ menu, menuHidden });
        if (menuHidden) menu.classList.remove("hidden-menu-md");
        else menu.classList.add("hidden-menu-md");
    });
}


// Articles
async function upvoteArticle(articleId, jwt, voteArrowElement) {
    const rawResponse = await fetch(`https://localhost:44300/api/articles/vote?articleId=${articleId}&upvote=true`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });

    const updatedArticle = await rawResponse.json();

    invertUpvoteStyle(voteArrowElement);

    const karmaElement = voteArrowElement.nextElementSibling;
    karmaElement.innerHTML = updatedArticle.karma;

    const downvoteArrowElement = karmaElement.nextElementSibling;
    clearDownvote(downvoteArrowElement);
}

function invertUpvoteStyle(voteArrowElement) {
    if (voteArrowElement.classList.contains("upvoted")) {
        voteArrowElement.classList.remove("upvoted");
    } else {
        voteArrowElement.classList.add("upvoted");
    }
}

function clearUpvote(voteArrowElement) {
    if (voteArrowElement.classList.contains("upvoted")) {
        voteArrowElement.classList.remove("upvoted");
    }
}

async function downvoteArticle(articleId, jwt, voteArrowElement) {
    const rawResponse = await fetch(`https://localhost:44300/api/articles/vote?articleId=${articleId}&upvote=false`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });

    const updatedArticle = await rawResponse.json();

    invertDownvoteStyle(voteArrowElement);

    const karmaElement = voteArrowElement.previousElementSibling;
    karmaElement.innerHTML = updatedArticle.karma;

    const upvoteArrowElement = karmaElement.previousElementSibling;
    clearUpvote(upvoteArrowElement);
}

function invertDownvoteStyle(voteArrowElement){
    if (voteArrowElement.classList.contains("downvoted")) {
        voteArrowElement.classList.remove("downvoted");
    } else {
        voteArrowElement.classList.add("downvoted");
    }
}

function clearDownvote(voteArrowElement) {
    if (voteArrowElement.classList.contains("downvoted")) {
        voteArrowElement.classList.remove("downvoted");
    }
}

async function saveArticle(articleId, jwt, saveArticleElement) {
    invertSaveText(saveArticleElement);
    const rawResponse = await fetch(`https://localhost:44300/api/users/save-article?articleId=${articleId}`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}

async function unsaveArticle(articleId, jwt, unsaveArticleElement) {
    await saveArticle(articleId, jwt, unsaveArticleElement);
}

function invertSaveText(saveArticleElement) {
    if (saveArticleElement.innerHTML === "Saved") {
        saveArticleElement.innerHTML = "Save"
    } else {
        saveArticleElement.innerHTML = "Saved";
    }
}

// Comments
async function upvoteComment(commentId, jwt, voteArrowElement) {
    const rawResponse = await fetch(`https://localhost:44300/api/comments/vote?commentId=${commentId}&upvote=true`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });

    const updatedComment = await rawResponse.json();

    invertUpvoteStyle(voteArrowElement);

    const karmaElement = voteArrowElement.nextElementSibling;
    karmaElement.innerHTML = updatedComment.karma;

    const downvoteArrowElement = karmaElement.nextElementSibling;
    clearDownvote(downvoteArrowElement);
}


async function downvoteComment(commentId, jwt, voteArrowElement) {
    const rawResponse = await fetch(`https://localhost:44300/api/comments/vote?commentId=${commentId}&upvote=false`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });

    const updatedComment = await rawResponse.json();

    invertDownvoteStyle(voteArrowElement);

    const karmaElement = voteArrowElement.previousElementSibling;
    karmaElement.innerHTML = updatedComment.karma;

    const upvoteArrowElement = karmaElement.previousElementSibling;
    clearUpvote(upvoteArrowElement);
}


async function saveComment(commentId, jwt, saveCommentElement) {
    invertSaveText(saveCommentElement);

    const rawResponse = await fetch(`https://localhost:44300/api/users/save-comment?commentId=${commentId}`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}

async function unsaveComment(commentId, jwt, unsaveCommentElement) {
    await saveComment(commentId, jwt, unsaveCommentElement);
}

