function fnSubmitResponse(title) {
    var isValid = true;
    var SurveyAnswer = {}; // Use an object instead of an array
    SurveyAnswer["SurveyTitle"] = title;
    SurveyAnswer["SurveyAnswers"] = []; // Change AnswerDetails to SurveyAnswers

    $(".form-control").each(function (index) {
        var questionType = $(this).attr('type') === 'text' ? 1 : 0;
        if (questionType === 1) {
            var answer = $(this).val();
            if (!answer) {
                isValid = false;
                $(this).css("border", "1px solid red");
                return;
            } else {
                $(this).css("border", "1px solid gray");
                SurveyAnswer["SurveyAnswers"].push({
                    Answer: answer,
                    QuestionType: questionType,
                    QuestionTitle: $(this).parent().find('.questionTitle').text()
                });
            }
        }
        else {
            //fetch the value from the radio button
            var answer = $(this).find('input[type="radio"]:checked').val();
            if (!answer) {
                isValid = false;
                $(this).css("border", "1px solid red");
                return;
            } else {
                $(this).css("border", "1px solid gray");
                SurveyAnswer["SurveyAnswers"].push({
                    Answer: answer,
                    QuestionType: questionType,
                    QuestionTitle: $(this).parent().find('.questionTitle').text()
                });
            }
        }       
    });

    if (isValid) {
        console.log(JSON.stringify(SurveyAnswer));
        //add ajax call to submit the response to the function SurveyAnswerController/Create
        $.ajax({
            url: '/SurveyAnswer/Create',
            type: 'POST',
            contentType: 'application/json; charset=utf-8', // Set the content type to JSON
            data: JSON.stringify(SurveyAnswer), // Serialize the object to JSON
            success: function (data) {
                alert('Your response has been saved successfully.');
                // Redirect to the home page
                window.location.replace('/Home/Index');
            },
            error: function (error) {
                console.error('There was some error saving your response:', {
                    message: error.responseText || error.statusText,
                    status: error.status,
                    stack: error.stack || 'No stack trace available'
                });
                alert('There was some error saving your response, kindly try again.');
            }
        });
    }
}
function fnCancel() {
    window.location.href = '/SurveyQuestion/Index';
}