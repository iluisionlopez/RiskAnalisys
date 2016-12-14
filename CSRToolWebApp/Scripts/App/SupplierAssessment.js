// Create the namespace for Assessmnet
CSRToolApp.createNamespcae("CSRToolApp.ASSESSMENT.SUPPLIER");

CSRToolApp.ASSESSMENT.SUPPLIER.Risk = function () {

    var privateQuestions = {};

    var GetIndexes = function (territory, version) {
        $.ajax({
            type: "GET",
            cache: false,
            url: "/Purchasing/GetIndexes",
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
            }
            else {
                alert(response.message);
            }

        }).fail(function () {
            alert("Something went wrong!");
        });
    };
    var Risk = function (territory, version, supply, sector, webscan, audit, qs) {
        var existing = false;
        if ($("#casetypeExistingSupplier").is(":checked"))
            existing = true;
        var postData = {
            selectedTerritory: territory,
            selectedVersion: version,
            isExinting: existing,
            selectedSupplyChain: supply,
            selectedSector: sector,
            selectedWebscanValues: webscan,
            selectedAuditValue: audit,
            questions: qs
        };
        $.ajax({
            type: "POST",
            cache: false,
            url: "/Purchasing/CalculateRisk",
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
                if(response.result)
                {
                    //alert("Risk: " + response.result.Item1 + " Performance: " + response.result.Item2);

                    $("span#matrix-icon").remove();
                    $('#RaitingComment').remove();
                    if (response.result.Item1 > 0 && response.result.Item2 > 0)
                    {
                        var span = '<span id="matrix-icon" class="icon-fullscreen" style="vertical-align: -10px; color:  #ffffff;"></span>';
                        var row = $("div#PerformaceResult div.row.matrix.show-grid:nth-child(" + response.result.Item1 + ")");
                        var cell = row.find("div.result-matrix:nth-child(" + response.result.Item2 + ")");
                        cell.append(span);
                        var color = '#aeca22';
                        var textRaiting = "A";
                        if (response.result.Item1 == response.result.Item2) {
                            $("span#matrix-icon").css("color", "#505050");
                            color = "#f7f72c";
                            textRaiting = "B";
                        }
                        if (response.result.Item1 < response.result.Item2) {
                            color = "#E6252A";
                            textRaiting = "C";
                        }
                        
                        var comment = '<div id="RaitingComment" class="col-md-12" style="height: 110px; padding:20% 0 0 30%; background-color:' + color + '"><span style="font-size: 48px;">' + textRaiting + '</span></div>';
                        $('#Raiting').append(comment);
                    }
                }
            }
            else {
                alert(response.message);
            }

        }).fail(function () {
            alert("Something went wrong!");
        });
    };
    var InitQuestions = function () {
        $.ajax({
            method: "GET",
            cache: false,
            url: "/Purchasing/GetQuestion",
            dataType: "json"
        })
        .done(function (response) { // jQuery 1.8 deprecates success() callback
            if (response.success) {
                SetQuestions(response.questions);
            }
        }).fail(function (response) {
            alert(response.message.toString());
        });
    };

    function GetQuestions() {
        return privateQuestions;
    };

    function SetQuestions(questions) {
        privateQuestions = questions;
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

    return {
        Indexes: function (territoryList, version) {
            if ($(territoryList) && $(territoryList).length) {
                var value = $(territoryList).val();
                GetIndexes(value, version);
            }
        },
        CalculateRisk: function (territoryList, version, supply, sector, webscan, audit, qs) {
            if ($(territoryList) && $(territoryList).length) {
                var value = $(territoryList).val();
                Risk(value, version, supply, sector, webscan, audit, qs);
            }
        },
        InitilaizeQuestions: function () {
            InitQuestions();
        },
        Questions: GetQuestions,
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
    var Risk = CSRToolApp.ASSESSMENT.SUPPLIER.Risk();
    var version = $("#hdnVersionID").val();
    Risk.InitilaizeQuestions();
    SetSelectedAnswers();
    ShowRaiting();

    if ($("#AssessmentID").val() == "")
    { $('a.btn-print-pdf').hide(); }

    $(document).delegate("#TerritoryId", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        Risk.Indexes($(evt.currentTarget), version);
        Risk.CalculateRisk($("#TerritoryId"), version, SetOptionToSelected($("#SupplyChain")).val(), SetOptionToSelected($("#SectorId")).val(), GetParametersValues("select.webscan option:selected"), SetOptionToSelected($("#CommentAudit")).val(), Risk.Questions());
    });

    /*Handle selected SupplyChain*/
    $(document).delegate("#SupplyChain", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        Risk.CalculateRisk($("#TerritoryId"), version, selected.val(), SetOptionToSelected($("#SectorId")).val(), GetParametersValues("select.webscan option:selected"), SetOptionToSelected($("#CommentAudit")).val(), Risk.Questions());
    });

    /*Handle selected Sector*/
    $(document).delegate("#SectorId", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        Risk.CalculateRisk($("#TerritoryId"), version, SetOptionToSelected($("#SupplyChain")).val(), selected.val(), GetParametersValues("select.webscan option:selected"), SetOptionToSelected($("#CommentAudit")).val(), Risk.Questions());
    });

    /*delegate the Event handler for case type */
    $(document).delegate("input[type=radio]", "click", function (evt) {
        evt.stopImmediatePropagation();
        Risk.CalculateRisk($("#TerritoryId"), version, SetOptionToSelected($("#SupplyChain")).val(), SetOptionToSelected($("#SectorId")).val(), GetParametersValues("select.webscan option:selected"), SetOptionToSelected($("#CommentAudit")).val(), Risk.Questions());
    });

    /*delegate the Event handler for change answer */
    $(document).delegate("select.answer-selector", "change", function (evt) {
        HandleSelectedAnswer(evt, this);
        var qs = Risk.Questions();
        if (!jQuery.isEmptyObject(qs)) {
            var selectedOption = $("option:selected", this);
            Risk.CalculateRisk($("#TerritoryId"), version, SetOptionToSelected($("#SupplyChain")).val(), SetOptionToSelected($("#SectorId")).val(), GetParametersValues("select.webscan option:selected"), SetOptionToSelected($("#CommentAudit")).val(), qs);
        }
    });

    /*delegate the Event handler for change supplier */
    $(document).delegate("#Supplier", "input", function (evt) {
        evt.stopImmediatePropagation();
        CreateWebSearchString($(evt.currentTarget), $("#WebSearch"));
    });

    $(document).delegate("textarea#Comment", "change keyup paste", function (evt) {
        evt.stopImmediatePropagation();
        var comment = $(evt.currentTarget).val();
        var raitingComment = $("#RatingComment").val(comment);
    });

    /*delegate the Event handler for change webscan */
    $(document).delegate("select.webscan", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        Risk.CalculateRisk($("#TerritoryId"), version, SetOptionToSelected($("#SupplyChain")).val(), SetOptionToSelected($("#SectorId")).val(), selected.val(), SetOptionToSelected($("#CommentAudit")).val(), Risk.Questions());
    });

    /*delegate the Event handler for change audit */
    $(document).delegate("select.audit", "change", function (evt) {
        evt.stopImmediatePropagation();
        var selected = SetOptionToSelected(this);
        Risk.SelectAnswer("AuditQuestionSupplier", $(selected).val());
        $('input#selectedCommentAudit').val($(selected).val());
        SetSelectedAnswers();
        Risk.CalculateRisk($("#TerritoryId"), version, SetOptionToSelected($("#SupplyChain")).val(), SetOptionToSelected($("#SectorId")).val(), GetParametersValues("select.webscan option:selected"), selected.val(), Risk.Questions());
    });

    /*Handle selected answer*/
    function HandleSelectedAnswer(evt, element) {
        evt.stopImmediatePropagation();
        var ddl = $(element);
        var selectedOption = SetOptionToSelected(element);
        //Risk.SelectAnswer($(ddl).attr("name"), $(selectedOption).val());
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

    function CreateWebSearchString(source, target) {
        var searchString = "environment OR labour OR human rights OR resettlement OR security";
        if ($(source).val() !== "") {
            $(target).val($(source).val() + " " + searchString);
        }
    }

    function SetOptionToSelected(element) {
        var ddl = $(element);
        var selected = $("option:selected", ddl);
        ddl.children("option").not(selected).removeAttr("selected");
        selected.attr("selected", "selected");

        return selected;
    }

    function GetParametersValues(selector) {
        var parameters = $(selector).map(function (i, e) {
            return $(e).val();
        }).get().join(",");
        return parameters;
    }

    function SetSelectedAnswers() {
        $('select.answer-selector').each(function (index, item) {
            var value = $('input:hidden[data-question="' + item.name + '"]').val();
            var option = $(item).children('option[value="' + value + '"]');
            option.attr('selected', 'selected');
            var selectedOption = SetOptionToSelected(item);
            Risk.SelectAnswer(item.name, $(selectedOption).val());
        })
        var auditvalue = $('input#selectedCommentAudit').val();
        var selectedAudit = $('#CommentAudit').children('option[value="' + auditvalue + '"]');
        selectedAudit.attr('selected', 'selected');
        Risk.SelectAnswer("AuditQuestionSupplier", $(selectedAudit).val());
    }

    function ShowRaiting() {
        var raiting = $("#hdnRiskValues");
        if (raiting)
        {
            var values = raiting.val().split(/[(,)]/);
            var item1 = values[1].trim();
            var item2 = values[2].trim();
            if (item1 > 0 && item2 > 0) {
                var span = '<span id="matrix-icon" class="icon-fullscreen" style="vertical-align: -10px; color:  #ffffff;"></span>';
                var row = $("div#PerformaceResult div.row.matrix.show-grid:nth-child(" + item1 + ")");
                var cell = row.find("div.result-matrix:nth-child(" + item2 + ")");
                cell.append(span);
                var color = '#aeca22';
                var textRaiting = "A";
                if (item1 === item2) {
                    $("span#matrix-icon").css("color", "#505050");
                    color = "#f7f72c";
                    textRaiting = "B";
                }
                if (item1 < item2) {
                    color = "#E6252A";
                    textRaiting = "C";
                }

                var comment = '<div id="RaitingComment" class="col-md-12" style="height: 110px; padding:20% 0 0 30%; background-color:' + color + '"><span style="font-size: 48px;">' + textRaiting + '</span></div>';
                $('#Raiting').append(comment);

            }
        }
        var raitingComment = $("#hdnRiskValues");
        raitingComment.val($('textarea#Comment').val());
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