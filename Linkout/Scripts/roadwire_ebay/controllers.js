'use strict';
/* Roadwire Ebay Controllers */

configure
.controller('RoadwireEbayCtrl', ['$scope', '$location', function ($scope, $location) {

    $scope.routeMenu = function () {
        $location.path('/menu');
    }
    $scope.routeUnshipped = function () {
        $location.path('/paidUnshipped');
    }

} ])

.controller('MenuCtrl', ['$scope', function ($scope) { } ])

.controller('PaidUnshippedCtrl', ['$scope', function ($scope) {

    var xmlReq = '<GeteBayOfficialTimeRequest xmlns="urn:ebay:apis:eBLBaseComponents">'
+'<RequesterCredentials>'
+'<eBayAuthToken>AgAAAA**AQAAAA**aAAAAA**9DJPUg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6wFk4GhC5CAogudj6x9nY+seQ**3V0CAA**AAMAAA**PcqkIx0m6ICTidyzGK3gC3XBer7ww1EUAHhl2EJEopHUCw6kygGDClF2AjEvZj3W21H/aRQfJfr5VSJ6ZZcUj6yb1V73FgmhWYFEEDr3pTEe4AXtBPQmxBNjj2DvfeueJoqT59dVJRGWjeqL/VvgyV7+j92sz0jwMIc6G6m4dmTeYRkDgZFgh2W12GadVv58Tka1iul8w2li0J0O27FNhuuk05fTdKKW4G0DQPz10YqPCP9yRcZwF3fnB246rH2YxjiDhdQHvi4JevnDiiW6D0ZcfAlDBnH8sjhbh7SrqtU+ngpkJlLga5jrwTyXIWYQJtKMB2bQxf2R0HQYxWmKqTocnJgQk2K/E738qggcZXVBFS9VczznHSoDAXSIhJSfZ7RqXeesF1ATadaCiK/yxdmk/pH4+jAiiAiMuw6k0L4g5kSkToE3jFrM7zS77GO1FeET+jQfoZ1UA/jqkFVcZ2DRqEXX7qSnUfIH6plNm6K+hwKv4qsqwLhPLnCChP0Oms0wZlpozMeTFlg7qbXYvsXOGigguhhoSZyiwFc0tdi5MX2B6WO66loHJuaSMfeTMVwztfzjF5rVUtHh0LU38Mgdg5kG+EYGsg+wectXZ1363U87KlO8CnGKGq9wPekdapyb/qJdfg8UmFtnryjseV39yn+Ee5IflOK9rT5ngO5rJTa3uAvFaT0N1/xUvZtPCQBDde5GMzbJtQMJJyMCU8NmlmwN2bGVJMmhz4vnMHXj7s7X2Pbevq9fcHMwzw/y</eBayAuthToken>'
+'</RequesterCredentials>'
+'</GeteBayOfficialTimeRequest>';

//    $.ajax({ type: "POST",
//        url: 'https://api.sandbox.ebay.com/wsapi',
//        data: xmlReq,
//        contentType: "text/xml",
//        dataType: "xml",
//        headers: {
//            'X-EBAY-API-COMPATIBILITY-LEVEL':'841',
//            'X-EBAY-API-DEV-NAME':'481891e7-46d4-4a19-8992-bbfef42842b7',
//            'X-EBAY-API-APP-NAME':'Roadwire-fb1b-4244-80f7-0a9a8f918293',
//            'X-EBAY-API-CERT-NAME':'99fc89de-c5a8-4594-97a5-9974d1908432',
//            'X-EBAY-API-SITEID':'0',
//            'X-EBAY-API-CALL-NAME':'GeteBayOfficialTime'
//        },
//        cache: false,
//        error: function (err) {
//            alert('err: ' + err); 
//        },
//        success: function (xml) {
//            alert("it works");
//            alert(xml);
//        }
//    });

    $.ajax({
        type: "POST",
        url: 'http://open.api.ebay.com/shopping',
        headers: {"X-Test-Header": "test-value"},
        dataType: "jsonp",
        jsonp: "callbackname",
        crossDomain: true,
        data: {
            'callname': 'FindItemsAdvanced',
            'appid': 'Roadwire-36ca-46dd-ac36-2e3a7ba40080',
            'version': '771',
            'siteid': '0',
            'requestencoding': 'JSON',
            'responseencoding': 'JSON',
            'QueryKeywords': 'pot shed',
            'MaxEntries': '3',
            'PriceMin': { 'Value': '250.0', 'CurrencyID': 'USD' },
            'PriceMax': { 'Value': '300.0', 'CurrencyID': 'USD' },
            'callback': true
        },
        success: function (object) {
            alert('success' + JSON.stringify(object, null, 4));
        },
        error: function (object, x, errorThrown) {
            alert("call failure");
        }
    });
                

} ])

;
