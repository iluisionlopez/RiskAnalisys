// Create the namespace for Assessmnet
CSRToolApp.createNamespcae("CSRToolApp.ASSESSMENT.CUSTOMER");

CSRToolApp.ASSESSMENT.CUSTOMER.CountryRisk = function () {
    var Indexes = function (territory, version, questions) {
        $.ajax({
            type: "GET",
            cache: false,
            url: "/Sales/PopulateIndexes",
            dataType: "json",
            data: { selectedTerritory: territory, selectedVersion: version }
        })
        .done(function (response) { // jQuery 1.8 deprecates success() callback
            if (response.success) {
                if (response.index) {
                    var divCoutry = $("div#CountryRiskAssessment");
                    divCoutry.html("");
                    divCoutry.html(response.index);
                }
                Risks(territory, version, "", questions);
            }
            else {
                alert(response.message);
            }

        }).fail(function (response) {
            alert(response.message);
        });
    };

    var Risks = function (territory, version, offer, qs, webscan) {
        var existing = false;
        var hidePeace = true;
        if ($("#casetypeExistingCustomer").is(":checked"))
            existing = true;
        $('.transaction-types input:checked').each(function () {
            if ($(this).data("type-name") === "Defense") {
                hidePeace = false;
            }
        });

        var postData = {
            selectedTerritory: territory,
            selectedVersion: version,
            selectedOfferTypes: offer,
            isExinting: existing,
            hidePeaceIndex: hidePeace,
            questions: qs,
            selectedWebscanValues: webscan
        };
        $.ajax({
            type: "POST",
            cache: false,
            url: "/Sales/CalculateRisk",
            dataType: "json",
            contentType: "application/json",
            traditional: true,
            data: JSON.stringify(postData)
        })
        .done(function (response) {
            if (response.success) {
                if (response.risk) {
                    var divResult = $("div#RiskResult");
                    divResult.html("");
                    divResult.html(response.risk);
                }
                if (response.riskMessage) {
                    var divResult = $("div#RiskMessageResult");
                    divResult.html("");
                    divResult.html(response.riskMessage);
                }
            }
            else {
                alert(response.message);
            }

        }).fail(function (response) {
            alert(response.message);
        });
    };

    return {
        Populate: function (territoryList, version, qs) {
            if ($(territoryList) && $(territoryList).length) {
                var value = $(territoryList).val();
                Indexes(value, version, qs);
            }
        },
        CalculateRisk: function (territoryList, version, parameters, questions, webscan) {
            var territory = "";
            if ($(territoryList) && $(territoryList).length) {
                var territory = $(territoryList).val();
            }
            Risks(territory, version, parameters, questions, webscan);
        }
    };
};

CSRToolApp.ASSESSMENT.CUSTOMER.QuestionRisk = function () {

    var privateQuestions = {};

    var GetQuestionByFilter = function (filter) {
        $.ajax({
            method: "GET",
            cache: false,
            url: "/Sales/GetQuestionByTransaction",
            dataType: "json",
            data: { parameters: filter }
        })
        .done(function (response) { // jQuery 1.8 deprecates success() callback
            if (response.success) {
                if (response.view) {
                    var divResult = $("div#SpecificQuestion");
                    divResult.html("");
                    divResult.html(response.view);
                    SetQuestions(response.questions);
                }
            }
        }).fail(function (response) {
            alert(response.message.toString());
        });
    };

    var InitQuestions = function (filter) {
        $.ajax({
            method: "GET",
            cache: false,
            url: "/Sales/GetQuestion",
            dataType: "json",
            data: { parameters: filter }
        })
        .done(function (response) { // jQuery 1.8 deprecates success() callback
            if (response.success) {
                var divResult = $("div#SpecificQuestion");
                SetQuestions(response.questions);
            }
        }).fail(function (response) {
            alert(response.message.toString());
        });
    };

    var SetAnswerToSelected = function (question, answer) {
        var questions = GetQuestions();
        if (questions) {
            $(questions).each(function (index, q) {
                if (q.Name === question) {
                    $(q.Answers).each(function (key, ans) {
                        if (answer > -1) {
                            if (ans.Value === answer) {
                                ans.Selected = true;
                                //alert(ans.Id);
                            }
                            else {
                                ans.Selected = false;
                            }
                        }
                        else
                            ans.Selected = false;
                    });
                }
            });
        }
    };

    function GetQuestions() {
        return privateQuestions;
    };

    function SetQuestions(questions) {
        privateQuestions = questions;
    };

    return {
        Initialize: function (parameters) {
            InitQuestions(parameters);
        },
        Filter: function (parameters) {
            GetQuestionByFilter(parameters);
        },
        Get: GetQuestions,
        SelectAnswer: function (question, answer) {
            SetAnswerToSelected(question, answer);
        }
    };
};

$(document).ready(function () {

    var element = $('.follow-scroll'), originalY = element.offset().top;
    // Space between element and top of screen (when scrolling)
    var topMargin = 25;
    // Should probably be set in CSS; but here just for emphasis
    element.css('position', 'relative');

    /* Create objects*/
    var RiskTerritory = CSRToolApp.ASSESSMENT.CUSTOMER.CountryRisk();
    var QuestionRisk = CSRToolApp.ASSESSMENT.CUSTOMER.QuestionRisk();
    QuestionRisk.Initialize(GetParametersByID(".transaction-types input:checked"));
    SetSelectedAnswers();
    var version = $("#hdnVersionID").val();
    var isDefenseChecked;

    if ($("#AssessmentID").val() == "")
    { $('a.btn-print-pdf').hide(); }

    /*Handle selected Territory*/
    $(document).delegate("#TerritoryId", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        RiskTerritory.Populate($(evt.currentTarget), version, QuestionRisk.Get());
    });

    /*Hadle selected Secotr*/
    $(document).delegate("#SectorId", "change", function (evt) {
        evt.stopImmediatePropagation();
        var ddl = $(this);
        var selected = SetOptionToSelected(this);
        //RiskTerritory.CalculateRisk(sector, version);
    })

    /*Handle selected transaction type*/
    $(document).delegate(".transaction-types input[type=checkbox]", "click", function (evt) {
        evt.stopImmediatePropagation();
        QuestionRisk.Filter(GetParametersByID(".transaction-types input:checked"));
        RiskTerritory.CalculateRisk($("#TerritoryId"), version, GetParametersByID(".offer-types input:checked"), QuestionRisk.Get(), GetParametersValues("select.webscan option:selected"));
    });

    /*Handle selected offer*/
    $(document).delegate(".offer-types input[type=checkbox]", "click", function (evt) {
        evt.stopImmediatePropagation();
        RiskTerritory.CalculateRisk($("#TerritoryId"), version, GetParametersByID(".offer-types input:checked"), QuestionRisk.Get(), GetParametersValues("select.webscan option:selected"));
    });

    /*delegate the Event handler for change answer */
    $(document).delegate("select.answer-selector", "change", function (evt) {
        HandleSelectedAnswer(evt, this);
        var qs = QuestionRisk.Get();
        if (!jQuery.isEmptyObject(qs)) {
            var selectedOption = $("option:selected", this);
            RiskTerritory.CalculateRisk($("#TerritoryId"), version, GetParametersByID(".offer-types input:checked"), qs, GetParametersValues("select.webscan option:selected"));
        }
    });

    /*delegate the Event handler for case type */
    $(document).delegate("input[type=radio]", "click", function (evt) {
        evt.stopImmediatePropagation();
        RiskTerritory.CalculateRisk($("#TerritoryId"), version, GetParametersByID(".offer-types input:checked"), QuestionRisk.Get(), GetParametersValues("select.webscan option:selected"));
    });

    /*delegate customer name lose focus*/
    $(document).delegate("#AssessmentCustomer", "input", function (evt) {
        evt.stopImmediatePropagation();
        CreateWebSearchString($(evt.currentTarget), $("#WebScan1"));
    });

    $(document).delegate("#Project", "input", function (evt) {
        evt.stopImmediatePropagation(); 
        CreateWebSearchString($(evt.currentTarget), $("#WebScan2"));
    });

    $(document).delegate("select.webscan", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        RiskTerritory.CalculateRisk($("#TerritoryId"), version, GetParametersByID(".offer-types input:checked"), QuestionRisk.Get(), GetParametersValues("select.webscan option:selected"));
    });

    function SetSelectedAnswers() {
        $('select.answer-selector').each(function (index, item) {
            var value = $('input:hidden[data-question="' + item.name + '"]').val();
            var option = $(item).children('option[value="' + value + '"]');
            option.attr('selected', 'selected');
            var selectedOption = SetOptionToSelected(item);
            SetAnswerToSelected(item.name, $(selectedOption).val());
        })
    }

    /*Handle selected answer*/
    function HandleSelectedAnswer(evt, element) {
        evt.stopImmediatePropagation();
        var ddl = $(element);
        var selectedOption = SetOptionToSelected(element);
        //SetAnswerToSelected($(ddl).attr("name"), $(selectedOption).val());
        var inputText = $("input[name='" + ddl.attr("name") + "']");
        var hiddenInput = $("input[data-question='" + ddl.attr("name") + "']");
        if (selectedOption.val() > -1) {
            inputText.val(selectedOption.val());
            inputText.html(selectedOption.val());
        }
        else {
            inputText.html("");
            inputText.val("");
        }
        hiddenInput.val(selectedOption.val());
        SetSelectedAnswers();
        inputText.attr("class", "form-control");
        var attrClass = "bg-color-riskfactor-" + selectedOption.val();
        inputText.addClass(attrClass);
    }

    function SetOptionToSelected(element) {
        var ddl = $(element);
        var selected = $("option:selected", ddl);
        ddl.children("option").not(selected).removeAttr("selected");
        selected.attr("selected", "selected");

        return selected;
    }

    /**/
    function SetAnswerToSelected(question, answer) {
        QuestionRisk.SelectAnswer(question, answer);
    }

    /**/
    function GetParametersByID(selector) {
        var parameters = $(selector).map(function (i, e) {
            return $(e).attr("id");
        }).get().join(",");
        return parameters;
    }

    function GetParametersValues(selector) {
        var parameters = $(selector).map(function (i, e) {
            return $(e).val();
        }).get().join(",");
        return parameters;
    }

    function CreateWebSearchString(source, target) {
        var searchString = "environment OR labour OR human rights OR resettlement OR security";
        if ($(source).val() !== "") {
            $(target).val($(source).val() + " " + searchString);
        }
    }

    // Event listener for scroll
    $(window).on('scroll', function (event) {
        if (document.documentElement.clientWidth > 1200) {
            var scrollTop = $(window).scrollTop();
            element.stop(false, false).animate({
                top: scrollTop < originalY ? 0 : scrollTop - originalY + topMargin
            }, 10);
        }
    });
});
