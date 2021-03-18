function updateBooks() {
    $.ajax({
        url: "books",
        type: "GET",
        success: function (result) {
            listBooks(result);
        }
    });
}

function getBook(id) {
    $.ajax({
        type: "GET",
        url: "books/" + id,
        success: function (result) {
            fillBookDataToForm(result);
        }
    });
}

function newBook(bookData) {
    $.ajax({
        type: "POST",
        url: "books",
        data: JSON.stringify(bookData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { updateBooks(); }
    });
}

function editBook(bookData) {
    $.ajax({
        type: "PUT",
        url: "books/" + bookData.id,
        data: JSON.stringify(bookData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { updateBooks(); }
    });
}

function deleteBook(bookData) {
    $.ajax({
        type: "DELETE",
        url: "books/" + bookData.id,
        success: function (data) { updateBooks(); }
    });
}

function listBooks(books) {
    $("#allBooks").html("<ul class='bookList'>")
    books.forEach(function (book) {
        $("<li class='bookLink' onclick='getBook(" + book.id + ")'>" + book.title + " | " + book.author + "</li>").appendTo("#allBooks");
    });
    $("</ul>").appendTo("#allBooks");
    document.getElementById("mainForm").reset();
}

function fillBookDataToForm(book) {
    $("#id").val(book.id);
    $("#title").val(book.title);
    $("#author").val(book.author);
    $("#description").val(book.description);
    enableButtons();
}

function getFormData() {
    var data = Object.fromEntries(new FormData(document.getElementById("mainForm")));
    return (data);
}

function disableButtons() {
    document.getElementById("save").disabled = true;
    document.getElementById("delete").disabled = true;
}

function enableButtons() {
    document.getElementById("save").disabled = false;
    document.getElementById("delete").disabled = false;
}

$(document).ready(function () {
    updateBooks();

    $("#saveNew").click(function () {
        var isValid = document.getElementById("mainForm").reportValidity();
        if (isValid) {
            var bookData = getFormData();
            delete bookData.id;             // When posting a new book, id must not be sent (as the book can't have it yet)
            newBook(bookData);
        }
    });

    $("#save").click(function () {
        var isValid = document.getElementById("mainForm").reportValidity();
        if (isValid) {
            var bookData = getFormData();
            editBook(bookData);
        }
    });

    $("#delete").click(function () {
        var bookData = getFormData();
        deleteBook(bookData);
    });

    $("#mainForm").on("reset", function () {
        disableButtons();
    });
});