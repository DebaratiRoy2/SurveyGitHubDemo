let questionCount = 0;
function createSurvey() {
    var isValid = true;
    var SurveyQuestion = {};

    if ($("#title").val() == '') {
        alert('Kindly enter the title of the survey.');
        isValid = false;
    }
    else {
        $.ajax({
            url: '/SurveyQuestion/GetSurveyDetailByTitle',
            type: 'GET',
            data: { title: $("#title").val() }, // Serialize the object to JSON
            contentType: 'application/json; charset=utf-8', // Set the content type to JSON
            success: function (response) {
                if (response == null) {
                    alert('There was some error checking title availability, kindly try again later.');
                    isValid = false;
                }
                else if (response) {
                    alert('There is already a survey with the same name, kindly type a different title');
                    isValid = false;
                }
                else {
                    SurveyQuestion["Title"] = $("#title").val();
                    SurveyQuestion["Description"] = $("#description").val();
                    SurveyQuestion["Questions"] = [];
                    for (let i = 1; i <= questionCount; i++) {
                        const questionText = $(`#question${i}Text`).val();
                        const questionType = $(`#question${i}Text`).data('type');
                        if (questionText.trim() !== '') {
                            SurveyQuestion["Questions"].push({
                                QuestionText: questionText,
                                Type: questionType === 'text' ? 1 : 0
                            });
                        }
                    }
                    if (SurveyQuestion["Questions"].length < 1) {
                        isValid = false;
                        alert('Select atleast one question to create the survey');
                    }
                }
                if (isValid) {
                    console.log(SurveyQuestion);
                    $.ajax({
                        url: '/SurveyQuestion/Create',
                        type: 'POST',
                        data: JSON.stringify(SurveyQuestion),
                        contentType: 'application/json; charset=utf-8',
                        success: function () {
                            alert('Your survey has been saved successfully.');
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
            },
            error: function (error) {
                console.error('There was some error saving your response:', {
                    message: error.responseText || error.statusText,
                    status: error.status,
                    stack: error.stack || 'No stack trace available'
                });
                alert('There was some error checking title availability, kindly try again later.');
            }
        });
    }
}
function addQuestion(type) {
    // Check if the previous question has details
    if (questionCount > 0) {
        const previousQuestion = document.getElementById(`question${questionCount}Text`);
        if (!previousQuestion.value.trim()) {
            alert('Please fill out the previous question before adding a new one.');
            return;
        }
    }
    questionCount++;
    const questionsContainer = document.getElementById('questionsContainer');
    const newQuestionDiv = document.createElement('div');
    newQuestionDiv.classList.add('card-body');
    if (type == 'text') {
        newQuestionDiv.innerHTML = `
            <label>Enter your description based Question</label>
            <input type="text" class="form-control" id="question${questionCount}Text" data-type="text"/>
        `;
    } else {
        newQuestionDiv.innerHTML = `
            <label>Enter your rating based Question</label>
            <input type="text" class="form-control" id="question${questionCount}Text" data-type="rating"/>
        `;
    }
    questionsContainer.appendChild(newQuestionDiv);
}

function navigateToDetails() {
    window.location.href = '/Home/Index';
}