﻿// Theme toggle
function setTheme(themeName) {
    localStorage.setItem('theme', themeName);
    document.documentElement.className = themeName;
}

function toggleTheme() {
    if (localStorage.getItem('theme') === 'theme-dark') {
        setTheme('theme-light');
    } else {
        setTheme('theme-dark');
    }
}

// Immediately invoked function to set the theme on initial load
(() => {
    if (localStorage.getItem('theme') === 'theme-dark') {
        setTheme('theme-dark');
    } else {
        setTheme('theme-light');
    }
})();

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
async function upvoteArticle(baseAddress, articleId, jwt, voteArrowElement) {
    // Update the style
    const karmaElement = voteArrowElement.nextElementSibling;
    const downvoteArrowElement = karmaElement.nextElementSibling;

    if (voteArrowElement.classList.contains("upvoted")) {
        voteArrowElement.classList.remove("upvoted");
        karmaElement.innerHTML = parseInt(karmaElement.innerHTML) - 1;

    } else {
        voteArrowElement.classList.add("upvoted");
        if (downvoteArrowElement.classList.contains("downvoted"))
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) + 2;
        else 
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) + 1;
    }

    clearDownvote(downvoteArrowElement);

    // Send a server update request
    await fetch(`${baseAddress}api/articles/vote?articleId=${articleId}&upvote=true`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}

function clearUpvote(voteArrowElement) {
    if (voteArrowElement.classList.contains("upvoted")) {
        voteArrowElement.classList.remove("upvoted");
    }
}

async function downvoteArticle(baseAddress, articleId, jwt, voteArrowElement) {
    // Update styles
    const karmaElement = voteArrowElement.previousElementSibling;
    const upvoteArrowElement = karmaElement.previousElementSibling;

    if (voteArrowElement.classList.contains("downvoted")) {
        voteArrowElement.classList.remove("downvoted");
        karmaElement.innerHTML = parseInt(karmaElement.innerHTML) + 1;
    } else {
        voteArrowElement.classList.add("downvoted");
        if (upvoteArrowElement.classList.contains("upvoted"))
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) - 2;
        else
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) - 1;
    }

    clearUpvote(upvoteArrowElement);

    // Send server req
    const rawResponse = await fetch(`${baseAddress}api/articles/vote?articleId=${articleId}&upvote=false`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}

function clearDownvote(voteArrowElement) {
    if (voteArrowElement.classList.contains("downvoted")) {
        voteArrowElement.classList.remove("downvoted");
    }
}

async function saveArticle(articleId, jwt, saveArticleElement) {
    invertSaveText(saveArticleElement);

    await fetch(`https://localhost:44300/api/users/save-article?articleId=${articleId}`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}


// Comments
async function upvoteComment(commentId, jwt, voteArrowElement) {
    const karmaElement = voteArrowElement.nextElementSibling;
    const downvoteArrowElement = karmaElement.nextElementSibling;

    if (voteArrowElement.classList.contains("upvoted")) {
        voteArrowElement.classList.remove("upvoted");
        karmaElement.innerHTML = parseInt(karmaElement.innerHTML) - 1;
    } else {
        voteArrowElement.classList.add("upvoted");
        if (downvoteArrowElement.classList.contains("downvoted")) 
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) + 2;
        else
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) + 1;
    }

    clearDownvote(downvoteArrowElement);

    await fetch(`https://localhost:44300/api/comments/vote?commentId=${commentId}&upvote=true`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}

async function downvoteComment(commentId, jwt, voteArrowElement) {
    const karmaElement = voteArrowElement.previousElementSibling;
    const upvoteArrowElement = karmaElement.previousElementSibling;

    if (voteArrowElement.classList.contains("downvoted")) {
        voteArrowElement.classList.remove("downvoted");
        karmaElement.innerHTML = parseInt(karmaElement.innerHTML) + 1;
    } else {
        voteArrowElement.classList.add("downvoted");
        if (upvoteArrowElement.classList.contains("upvoted"))
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) - 2;
        else
            karmaElement.innerHTML = parseInt(karmaElement.innerHTML) - 1;
    }

    clearUpvote(upvoteArrowElement);

    await fetch(`https://localhost:44300/api/comments/vote?commentId=${commentId}&upvote=false`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": `Bearer ${jwt}`
        },
        body: "",
    });
}

async function saveComment(commentId, jwt, saveCommentElement) {
    invertSaveText(saveCommentElement);

    await fetch(`https://localhost:44300/api/users/save-comment?commentId=${commentId}`, {
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

function invertSaveText(saveArticleElement) {
    if (saveArticleElement.innerHTML === "Saved") {
        saveArticleElement.innerHTML = "Save"
        saveArticleElement.classList.remove("text-info");
    } else {
        saveArticleElement.innerHTML = "Saved";
        saveArticleElement.classList.add("text-info");
    }
}
