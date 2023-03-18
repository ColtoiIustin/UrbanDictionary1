const button = document.querySelector('.menu-place');
const click = document.getElementById('thedropdown');

button.addEventListener('click', (event) => {
    event.preventDefault();
    if (click.style.display === "none") {
        console.log("e none")
        click.style.display = "block";
    } else {
        click.style.display = "none";
    }
});