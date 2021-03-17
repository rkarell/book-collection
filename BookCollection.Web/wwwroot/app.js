function updateBooks() {
    $.ajax({
        url: "http://localhost:8080/books",
        type: "GET",
        success: function (result) {
            listBooks(result);
        }
    });
}

function listBooks(books) {
    $("#allBooks").html("<ul class='bookList'>")
    books.forEach(function (book) {
        $("<li class='bookLink' onclick='getBook(" + book.id + ")'>" + book.title + ", " + book.author + "</li>").appendTo("#allBooks");
    });
    $("</ul>").appendTo("#allBooks");
    document.getElementById("mainForm").reset();
}

function getBook(id) {
    $.ajax({
        type: "GET",
        url: "http://localhost:8080/books/" + id,
        success: function (result) {
            fillBookData(result);
        }
    });
}

function fillBookData(book) {
    $("#id").val(book.id);
    $("#title").val(book.title);
    $("#author").val(book.author);
    $("#description").val(book.description);
}

function getFormData() {
    var data = Object.fromEntries(new FormData(document.getElementById("mainForm")));
    return (data);
}

function newBook(bookData) {
    $.ajax({
        type: "POST",
        url: "http://localhost:8080/books",
        data: JSON.stringify(bookData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { updateBooks(); },
        error: function (errMsg) {
            alert(errMsg);
        }
    });
}

function editBook(bookData) {
    $.ajax({
        type: "PUT",
        url: "http://localhost:8080/books/" + bookData.id,
        data: JSON.stringify(bookData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) { updateBooks();},
        error: function (errMsg) {
            alert(errMsg);
        }
    });
}

function deleteBook(bookData) {
    $.ajax({
        type: "DELETE",
        url: "http://localhost:8080/books/" + bookData.id,
        success: function (data) { updateBooks();},
        error: function (errMsg) {
            alert(errMsg);
        }
    });
}

$(document).ready(function () {
    updateBooks();

    $("#saveNew").click(function () {
        var bookData = getFormData();
        delete bookData.id;
        newBook(bookData);
    });

    $("#save").click(function () {
        var bookData = getFormData();
        editBook(bookData);
    });

    $("#delete").click(function () {
        var bookData = getFormData();
        deleteBook(bookData);
    });
});