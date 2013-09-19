function BlindVM(blindLookupApi, blindBaseApi) {
    var _this = this;
    
    _this.items = ko.observableArray();

    _this.orderBy = ko.observable('Name');

    _this.orderBy.subscribe(function () {
        _this.search();
    });    
   
    _this.search = function () {
        var call = $.getJSON(blindBaseApi, { selectedBlind: _this.selectedBlind(), orderBy: _this.orderBy() });
        call.success(function (data) {
            var items = ko.mapping.fromJS(data.items);
            _this.items(items());
        });
        call.error(function (jqXHR) {
            showMessage('Error', jqXHR.responseText, '60%');
        });
    };
        
    /*------------- Autocomlete -----------------*/
    _this.selectedBlind     = ko.observable();

    _this.blindArray        = ko.observableArray(); // for autocomplete list      

    _this.getBlinds = function (searchTerm, sourceArray) {
        var total = 10;
        var url = blindLookupApi + '?term=' + searchTerm + '&total=' + total;

        $.ajax({
            url      : url,
            type     : 'GET',
            cache    : false,
            dataType : 'json',
            minLength: 1,
            delay    : 3000,
            success  : function (json) {
                sourceArray(json);
            },
            error: function (jqXHR) {          
                showMessage('Error', jqXHR.responseText, '60%');
            }
        })
    };

    /*------------- Autocomlete -----------------*/
}