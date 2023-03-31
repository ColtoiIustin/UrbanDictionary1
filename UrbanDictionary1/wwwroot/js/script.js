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

        $.ajax({
            method: 'POST',
            url: '/Expressions/LikeDislike',
            data: { expId: expId, likeType: 'like' },
            success: function (result) {
                // Update the UI to show the new like count for the specific post
                const expContainer = $('#expContainer_' + result.expId);
                expContainer.find('.likes').text(result.likes);
                expContainer.find('.dislikes').text(result.dislikes);
                $('#buton').text(result.likes);
                $('#spa').text(result.dislikes);
            },
            error: function (xhr, status, error) {
                // Handle errors
            }
        });
    });


    $('.dislike-part').click(function () {
        var expId = $(this).data('exp-id');

        $.ajax({
            method: 'POST',
            url: '/Expressions/LikeDislike',
            data: { expId : expId , likeType : 'dislike' },
            success: function (result) {
                // Update the UI to show the new like count for the specific post
                const expContainer = $('#expContainer_' + result.expId);
                expContainer.find('.likes').text(result.likes);
                expContainer.find('.dislikes').text(result.dislikes);
                $('#buton').text(result.likes);
                $('#spa').text(result.dislikes);
            },
            error: function (xhr, status, error) {
                // Handle errors
            }
        });
    });

$('#buton').click(function () {
    expId = 18;
    $.ajax({
        method: 'POST',
        url: '/Expressions/Mue',
        data: {  },
        success: function (result) {
            // Update the UI to show the new like count for the specific post
            
            $('#buton').text(result.cucu);
            $('#spa').text(result.cucu2);

            const expContainer = $('#expContainer_' + result.id);
            expContainer.find('.likes').text(result.cucu2);
        },
        error: function (xhr, status, error) {
            // Handle errors
        }
    });
});
