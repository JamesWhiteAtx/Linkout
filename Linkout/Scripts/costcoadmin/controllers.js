'use strict';
/* Costco Controllers */

costco
.controller('CostcoCtrl', ['$rootScope', '$scope', '$location', function ($rootScope, $scope, $location) {

    $rootScope.loading = false;
    $scope.startLoading = function () {
        $rootScope.loading = true;
    }
    $scope.endLoading = function () {
        $rootScope.loading = false;
    }

    $scope.setHeading = function (heading) {
        $rootScope.heading = heading;
    }
    $scope.setHeading(null);


    $scope.routeMenu = function () {
        $location.path('/menu');
    }
    $scope.routeDownloadOrders = function () {
        $location.path('/downloadorders');
    }
    $scope.routeDownloadOrder = function (name) {
        $location.path('/downloadorder' + name);
    }

    $scope.routeLocalConfirms = function () {
        $location.path('/localconfirms');
    }
    $scope.routeNewConfirm = function () {
        $location.path('/newconfirm');
    }
    $scope.routeEditConfirm = function (name) {
        $location.path('/editconfirm/' + name);
    }

    $scope.routeViewOrders = function () {
        $location.path('/vieworders');
    }
    $scope.routeViewOrder = function (name) {
        $location.path('/vieworder/' + name);
    }

    $scope.routeLocalAcks = function () {
        $location.path('/localacks');
    }
    $scope.routeEditAck = function (name) {
        $location.path('/editack/' + name);
    }
    $scope.routeNewAck = function () {
        $location.path('/newack');
    }
    $scope.routeOrderAck = function (cached) {
        $location.path('/orderack/' + cached);
    }


} ])
.controller('CcMenuCtrl', ['$scope', function ($scope) {
    $scope.setHeading("Costco Admin");
} ])

.controller('FtpOrdersCtrl', ['$scope', 'Download', function ($scope, Download) {
    $scope.setHeading("Download Orders");
    $scope.err = null;
    $scope.files = null;

    $scope.startLoading();
    Download.orders().then(
        function (results) {
            $scope.files = results;
            $scope.endLoading();
        },
        function (result) {
            $scope.err = result;
            $scope.endLoading();
        }
    );

    $scope.downloadFile = function (fileInfo) {
        $scope.routeDownloadOrder(fileInfo.Filename);
    };
} ])

.controller('FtpOrderCtrl', ['$scope', '$routeParams', 'Download', function ($scope, $routeParams, Download) {
    $scope.fileName = $routeParams.name;

    $scope.setHeading("Download " + $scope.fileName);
    $scope.batch = null;
    $scope.err = null;
    $scope.msg = null;

    $scope.startLoading();
    Download.orders($scope.fileName).then(
        function (file) {
            $scope.file = file;
            $scope.endLoading();
        },
        function (err) {
            $scope.err = err;
            $scope.endLoading();
        }
    );

} ])

.controller('ViewOrdersCtrl', ['$scope', 'Read', 'Decrypt', function ($scope, Read, Decrypt) {
    $scope.setHeading("View Orders");
    $scope.err = null;
    $scope.files = null;

    $scope.startLoading();
    Read.orders().then(
        function (results) {
            $scope.files = results;
            $scope.endLoading();
        },
        function (result) {
            $scope.err = result;
            $scope.endLoading();
        }
    );

    $scope.viewOrder = function (file) {
        $scope.routeViewOrder(file.Name);
    };

    $scope.decryptOrder = function (file) {
        var decryp = function (encryptName) {
            $scope.startLoading();
            Decrypt.order(encryptName).then(
                function (decryptedFile) {
                    if ((decryptedFile) && (decryptedFile.Exists) && (file.ID == decryptedFile.ID)) {
                        file.Exists = decryptedFile.Exists;
                        file.Name = decryptedFile.Name;
                        file.Path = decryptedFile.Path;
                        file.FileDateTime = decryptedFile.FileDateTime;
                    }
                    $scope.endLoading();
                },
                function (result) {
                    $scope.err = result;
                    $scope.endLoading();
                }
            );
        }
        if (file.Exists) {
            if (confirm("Are you sure you want to decrypt " + file.EncryptName + " and overwrite the existing " + file.Name + " file?")) {
                decryp(file.EncryptName);
            };
        } else {
            decryp(file.EncryptName);
        };
    };

} ])

.controller('ViewOrderCtrl', ['$scope', '$routeParams', 'Read', 'OrderCache', function ($scope, $routeParams, Read, OrderCache) {
    $scope.fileName = $routeParams.name;
    $scope.setHeading("Order Batch " + $scope.fileName);
    $scope.err = null;
    $scope.files = null;

    $scope.startLoading();
    Read.orders($scope.fileName).then(
        function (batch) {
            $scope.batch = batch;
            $scope.endLoading();
        },
        function (err) {
            $scope.err = err;
            $scope.endLoading();
        }
    );

    $scope.orderAck = function (batch) {
        OrderCache.put(batch.FileName, batch);
        $scope.routeOrderAck(batch.FileName);
    }
} ])

.controller('LocalConfirmsCtrl', ['$scope', 'Read', 'Encrypt', 'Upload', function ($scope, Read, Encrypt, Upload) {
    $scope.setHeading("Confirmations");
    $scope.err = null;
    $scope.files = null;

    $scope.startLoading();
    Read.confirms().then(
        function (results) {
            $scope.files = results;
            $scope.endLoading();
        },
        function (result) {
            $scope.err = result;
            $scope.endLoading();
        }
    );

    $scope.editConfirm = function (file) {
        $scope.routeEditConfirm(file.Name);
    };

    $scope.encryptConfirm = function (file) {
        var encryp = function (decryptName) {
            $scope.startLoading();
            Encrypt.confirm(decryptName).then(
                function (encryptedFile) {
                    if ((encryptedFile) && (encryptedFile.EncryptExists) && (file.ID == encryptedFile.ID)) {
                        file.EncryptExists = encryptedFile.EncryptExists;
                        file.EncryptName = encryptedFile.EncryptName;
                        file.EncryptPath = encryptedFile.EncryptPath;
                        file.EncryptDateTime = encryptedFile.EncryptDateTime;
                    }
                    $scope.endLoading();
                },
                function (result) {
                    $scope.err = result;
                    $scope.endLoading();
                }
            );
        }
        if (file.EncryptExists) {
            if (confirm("Are you sure you want to encrypt " + file.Name + " and overwrite the existing " + file.EncryptName + " file?")) {
                encryp(file.Name);
            };
        } else {
            encryp(file.Name);
        };
    };

    $scope.uploadConfirm = function (file) {
        $scope.startLoading();
        Upload.confirm(file.EncryptName).then(
            function (result) {
                $scope.endLoading();
            },
            function (result) {
                $scope.err = result;
                $scope.endLoading();
            }
        );

    };
} ])

.controller('EditConfirmCtrl', ['$scope', '$routeParams', 'Read', 'ConfService', 'Save', function ($scope, $routeParams, Read, ConfService, Save) {
    $scope.err = null;
    $scope.batch = null;

    $scope.shipcodes = [
        { value: '', label: '<Shipping Code>' },
        { value: 'UPSN_SE', label: 'UPS 2nd Day Air' },
        { value: 'UPSN_3D', label: 'UPS 3 Day Select' },
        { value: 'UPSC_ND', label: 'UPS Express Saver Canada' },
        { value: 'UPGS', label: 'UPS Freight (service level unspecified)' },
        { value: 'UPGF_G2', label: 'UPS Freight - Basic Service' },
        { value: 'UPSN_CG', label: 'UPS Ground' },
        { value: 'UPSN_FC', label: 'UPS Mail Innovations - Standard' },
        { value: 'UPSN_ND', label: 'UPS Next Day Air' },
        { value: 'UPSN_PM', label: 'UPS Next Day Air Saver' }
    ];

    $scope.fileName = $routeParams.name;

    if ($scope.fileName) {
        $scope.setHeading("Edit Confirm Batch " + $scope.fileName);
        $scope.startLoading();
        Read.confirms($scope.fileName).then(
            function (jsonBatch) {
                $scope.batch = ConfService.batchFromJson(jsonBatch);
                $scope.endLoading();
            },
            function (err) {
                $scop.batch = null;
                $scope.err = err;
                $scope.endLoading();
            }
        );
    } else {
        $scope.setHeading("New Confirm Batch");
        $scope.batch = ConfService.makeConfBatch();
    };

    $scope.dlgAct = null;
    $scope.dlgPackageIDs = null;
    $scope.dlgActPkg = function (act, conf) {
        $scope.dlgAct = act;
        $scope.dlgPackageIDs = $.map(conf.Packages, function (elm, idx) {
            if (act.PackageIDs.indexOf(elm.ID) == -1) {
                return elm.ID;
            }
        });
        if ($scope.dlgPackageIDs.length > 0) {
            $('#dlgPkg').modal('show');
        };
    };

    $scope.addActPkg = function (id) {
        $('#dlgPkg').modal('hide');
        if ($scope.dlgAct) {
            $scope.dlgAct.PackageIDs.push(id);
        };
    };

    $scope.saveBatch = function () {
        var fileName = $scope.batch.FileName;
        var newFileName = $scope.batch.NewFileName;

        if ((!fileName) && (!newFileName)) {
            alert("You must specify a file name.");
            return;
        };

        if ((!fileName) && (newFileName)) {
            var ext = newFileName.split('.').pop().trim().toUpperCase();
            if (ext != "XML") {
                $scope.batch.NewFileName = newFileName.trim() + ".xml";
            };
        };

        var jsonModel = { json: JSON.stringify($scope.batch) };

        $scope.startLoading();
        Save.confirm(jsonModel).then(
            function (jsonBatch) {
                $scope.batch = ConfService.batchFromJson(jsonBatch);
                $scope.endLoading();
            },
            function (err) {
                $scope.err = err;
                $scope.endLoading();
            }
        );

    };

} ])

.controller('LocalAcksCtrl', ['$scope', 'Read', 'Encrypt', 'Upload', function ($scope, Read, Encrypt, Upload) {
    $scope.setHeading("Acknowledgments");
    $scope.err = null;
    $scope.files = null;

    $scope.startLoading();
    Read.acks().then(
        function (results) {
            $scope.files = results;
            $scope.endLoading();
        },
        function (result) {
            $scope.err = result;
            $scope.endLoading();
        }
    );

    $scope.editAck = function (file) {
        $scope.routeEditAck(file.Name);
    };

    $scope.encryptAck = function (file) {
        var encryp = function (decryptName) {
            $scope.startLoading();
            Encrypt.ack(decryptName).then(
                function (encryptedFile) {
                    if ((encryptedFile) && (encryptedFile.EncryptExists) && (file.ID == encryptedFile.ID)) {
                        file.EncryptExists = encryptedFile.EncryptExists;
                        file.EncryptName = encryptedFile.EncryptName;
                        file.EncryptPath = encryptedFile.EncryptPath;
                        file.EncryptDateTime = encryptedFile.EncryptDateTime;
                    }
                    $scope.endLoading();
                },
                function (result) {
                    $scope.err = result;
                    $scope.endLoading();
                }
            );
        }
        if (file.EncryptExists) {
            if (confirm("Are you sure you want to encrypt " + file.Name + " and overwrite the existing " + file.EncryptName + " file?")) {
                encryp(file.Name);
            };
        } else {
            encryp(file.Name);
        };
    };

    $scope.uploadAck = function (file) {
        $scope.startLoading();
        Upload.ack(file.EncryptName).then(
            function (result) {
                $scope.endLoading();
            },
            function (result) {
                $scope.err = result;
                $scope.endLoading();
            }
        );

    };
} ])

.controller('EditAckCtrl', ['$scope', '$routeParams', 'Read', 'AckService', 'Save', 'OrderCache', function ($scope, $routeParams, Read, AckService, Save, OrderCache) {
    $scope.err = null;
    $scope.batch = null;

    $scope.setHeading("Edit Acknowledgment Batch...");
    $scope.startLoading();

    AckService.loadBatch($routeParams.name, $routeParams.cached)
        .then(
            function (batch) {
                var fileName = null;
                if (batch) {
                    fileName = batch.FileName;
                    $scope.batch = batch;
                } else {
                    $scope.batch = AckService.makeAckBatch();
                };
                if (fileName) {
                    $scope.setHeading("Edit Acknowledgment Batch " + fileName);
                } else {
                    $scope.setHeading("New Acknowledgment Batch");
                };

                $scope.endLoading();
            },
            function (err) {
                $scope.batch = null;
                $scope.err = err;
                $scope.endLoading();
            }
        );

    $scope.saveBatch = function () {
        var fileName = $scope.batch.FileName;
        var newFileName = $scope.batch.NewFileName;

        if ((!fileName) && (!newFileName)) {
            alert("You must specify a file name.");
            return;
        };

        if ((!fileName) && (newFileName)) {
            var ext = newFileName.split('.').pop().trim().toUpperCase();
            if (ext != "XML") {
                $scope.batch.NewFileName = newFileName.trim() + ".xml";
            };
        };

        var jsonModel = { json: JSON.stringify($scope.batch) };

        $scope.startLoading();
        Save.ack(jsonModel).then(
            function (jsonBatch) {
                $scope.batch = AckService.batchFromJson(jsonBatch);
                $scope.endLoading();
            },
            function (err) {
                $scope.err = err;
                $scope.endLoading();
            }
        );

    };

} ])

;
