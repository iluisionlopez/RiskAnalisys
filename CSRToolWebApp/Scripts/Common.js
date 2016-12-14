$(document).ready(function () {    

    // Event listener for close or remove
    $(document).on("click", "[removeParentOnClick]", function () {
        //  Close user message
        $(this).parent().remove();
    });

    // Event listener for submit
    $(document).on("click", '[type="submit"][confirmSubmitMessage]', function () {
        var confirmMessage = $(this).attr("confirmSubmitMessage");
        if (confirmMessage.length > 0) {
            if (!confirm(confirmMessage)) {
                return false;
            }
        }
    });
    
    
});