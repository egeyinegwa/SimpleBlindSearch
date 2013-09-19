function showMessage(title, message, width) {
    var msg = '<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>' + message + '</p>';
    $dialog = $('<div></div>')
                    .html(msg)
                    .dialog({
                        autoOpen: false,
                        title: title,
                        height: "auto",
                        width: width
                    });

    $dialog.dialog('open');
    return false;
}