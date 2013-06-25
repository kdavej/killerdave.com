function GetStudent(sid) {
    $.post('/code/views/ajax/getstudentdetails.aspx',
    {
        studentID: sid
    }, function (html) {
        $('#student-details').html(html);

    });
}

function UpdateStudent(sid) {

    $.post('/code/views/ajax/UpdateStudent.aspx',
    {
        StudentID: sid,
        FirstName: $('#fname').val(),
        LastName: $('#lname').val(),
        Birthday: $('#bdate').val(),
        PomLevel: $('#plevel').val(),
        P1FirstName: $('#p1fname').val(),
        P1LastName: $('#p1lname').val(),
        P2FirstName: $('#p2fname').val(),
        P2LastName: $('#p2lname').val(),
        Phone1: $('#phone1').val(),
        Phone2: $('#phone2').val(),
        EmailAddress1: $('#email1').val(),
        EmailAddress2: $('#email2').val(),
        AddressLine1: $('#addr1').val(),
        City: $('#city').val(),
        State: $('#state').val(),
        zip: $('#zip').val(),
        Active: document.getElementById('Active').checked,
        POnly: document.getElementById('POnly').checked,
        DNE: document.getElementById('DNE').checked
    }, function (html) {
        alert(html);
    });
}