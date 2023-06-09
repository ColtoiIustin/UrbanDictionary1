//Meniu
const openNav = document.querySelector('.menu-bar button');
const closeNav = document.querySelector('.close-nav button');
const Navbar = document.querySelector('.navbar');

window.addEventListener("resize", (event) => {
    if (Navbar.classList.contains('show')) {
        showHide();
    }
});

openNav.addEventListener("click", (event) => {
    showHide();
    document.body.style.overflowY = 'hidden';
});

closeNav.addEventListener("click", (event) => {
    showHide();
    document.body.style.overflowY = 'visible';
});

function showHide() {
    Navbar.classList.toggle('show');
}


//jQuery ev handler sending an AJAX request



    $('.like-part').click(function () {
        var expId = $(this).data('exp-id');
        const container = $('#expContainer_' + expId);
        var likeButton = container.find('.like-part');
        var dislikeButton = container.find('.dislike-part');
        
        $.ajax({
            method: 'POST',
            url: '/Expressions/LikeDislike',
            data: { expId: expId, likeType: 'like' },
            success: function (result) {
                if (result.userAuthenticated === 'false')
                    window.location.href = "/Identity/Account/Login";
                else {

                    // Update the UI to show the new like count for the specific post
                    const expContainer = $('#expContainer_' + result.expId);
                    expContainer.find('.likes').text(result.likes);
                    expContainer.find('.dislikes').text(result.dislikes);

                    if (result.action === 'none-like') {
                        dislikeButton.css('background-color', '');
                        likeButton.css('background-color', '#3c7334');
                        likeButton.css('border-radius', '0.5em');
                    } else if (result.action === 'like-like') {
                        dislikeButton.css('background-color', '');
                        likeButton.css('background-color', '');
                    } else if (result.action === 'dislike-like') {
                        dislikeButton.css('background-color', '');
                        likeButton.css('background-color', '#3c7334');
                        likeButton.css('border-radius', '0.5em');
                    }
                }
                
            },
            error: function (xhr, status, error) {
                // Handle errors
            }
        });
    });


    $('.dislike-part').click(function () {
        var expId = $(this).data('exp-id');
        const container = $('#expContainer_' + expId);
        var likeButton = container.find('.like-part');
        var dislikeButton = container.find('.dislike-part');

        $.ajax({
            method: 'POST',
            url: '/Expressions/LikeDislike',
            data: { expId : expId , likeType : 'dislike' },
            success: function (result) {
                if (result.userAuthenticated === 'false')
                    window.location.href = "/Identity/Account/Login";
                else {
                    // Update the UI to show the new like count for the specific post
                    const expContainer = $('#expContainer_' + result.expId);
                    expContainer.find('.likes').text(result.likes);
                    expContainer.find('.dislikes').text(result.dislikes);

                    if (result.action === 'none-dislike') {
                        likeButton.css('background-color', '');
                        dislikeButton.css('background-color', '#994d45');
                        dislikeButton.css('border-radius', '0.5em');
                    } else if (result.action === 'dislike-dislike') {
                        likeButton.css('background-color', '');
                        dislikeButton.css('background-color', '');
                    } else if (result.action === 'like-dislike') {
                        likeButton.css('background-color', '');
                        dislikeButton.css('background-color', '#994d45');
                        dislikeButton.css('border-radius', '0.5em');
                    }
                }
            },
            error: function (xhr, status, error) {
                // Handle errors
            }
        });
    });

$(document).ready(function () {
    // Loop through each post on the page
    $('.expression-card').each(function () {
        var expId = $(this).data('exp-id');
        var likeButton = $(this).find('.like-part');
        var dislikeButton = $(this).find('.dislike-part');
        var likesSystem = $(this).find('.likes-system');

        // Make an AJAX request to get the user's action for the post
        $.get('/Expressions/GetUserActionForPost', { expId: expId }, function (data) {

            if (data.userAuthenticated != 'false') {

                // Update the button color based on the user's action
                if (data.action === 'like') {
                    likeButton.css('background-color', '#3c7334');
                    likeButton.css('border-radius', '0.5em');
                    likesSystem.css()
                } else if (data.action === 'dislike') {
                    dislikeButton.css('background-color', '#994d45');
                    dislikeButton.css('border-radius', '0.5em');
                }
            }
        });
    });

});



    $(function () {
        $("#search").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Expressions/SearchPosts",
                    type: "GET",
                    dataType: "json",
                    data: { term: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.name, value: item.id };
                        }));
                    }
                });
            },
            minLength: 1,
            delay: 500,
            select: function (event, ui) {
                window.location.href = "/Expressions/SearchExpression/" + ui.item.value;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                .append("<div>" + item.label + "</div>")
                .appendTo(ul);
        };
    });

