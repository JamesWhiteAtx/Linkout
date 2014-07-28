'use strict';
/* Costco Services */

angular.module('costco.services', ['ngResource'])

.factory('Download', ['$q', '$http', function ($q, $http) {
    return {
        orders: function (name) {
            var delay = $q.defer();
            var url = '/ccdownload/orders';
            if (name) {
                url = url + '/' + name + '/';
            };

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }
    };
}])

.factory('Read', ['$q', '$http', function ($q, $http) {
    return {
        orders: function (name) {
            var delay = $q.defer();
            var url = '/ccread/orders';
            if (name) {
                url = url + '/' + name + '/';
            };

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        },
        confirms: function (name) {
            var delay = $q.defer();
            var url = '/ccread/confirms';
            if (name) {
                url = url + '/' + name + '/';
            };

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        },
        acks: function (name) {
            var delay = $q.defer();
            var url = '/ccread/fas';
            if (name) {
                url = url + '/' + name + '/';
            };

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }
    };
} ])

.factory('Decrypt', ['$q', '$http', function ($q, $http) {
    return {
        order: function (name) {
            var delay = $q.defer();
            var url = '/ccdecrypt/order/' + name + '/';

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }
    };
}])

.factory('Encrypt', ['$q', '$http', function ($q, $http) {
    return {
        confirm: function (name) {
            var delay = $q.defer();
            var url = '/ccencrypt/confirm/' + name + '/';

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }
        ,
        ack: function (name) {
            var delay = $q.defer();
            var url = '/ccencrypt/FA/' + name + '/';

            $http.get(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }

    };
}])

.factory('Save', ['$q', '$http', function ($q, $http) {
    return {

        confirm: function (jsonModel) {
            var delay = $q.defer();
            var url = '/ccsave/confirm/';

            $http.post(url, jsonModel)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        },

        ack: function (jsonModel) {
            var delay = $q.defer();
            var url = '/ccsave/FA/';

            $http.post(url, jsonModel)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        },

    };
}])

.factory('Upload', ['$q', '$http', function ($q, $http) {
    return {
        confirm: function (name) {
            var delay = $q.defer();
            var url = '/ccupload/confirm/' + name + '/';

            $http.put(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }
        ,
        ack: function (name) {
            var delay = $q.defer();
            var url = '/ccupload/FA/' + name + '/';

            $http.put(url)
            .success(function (result) {
                delay.resolve(result);
            })
            .error(function (obj) {
                delay.reject(obj);
            });

            return delay.promise;
        }
    };
}])

.factory('OrderCache', ['$cacheFactory', function ($cacheFactory) {
    return $cacheFactory('orderCache');
} ])

.factory('ConfService', [function () {

    var makeConfPackage = function (id) {
        var self = {
            ID: id,
            ShipDate: null,
            ServiceLevel1: null,
            TrackingNumber: null,
            Weight: null,
        };
        return self;
    };

    var makeConfAction = function (id) {
        var self = {
            ID: id,
            Shipment: true,
            ActionCode: null,
            MerchantLineNumber: null,
            TrxVendorSKU: null,
            TrxMerchantSKU: null,
            TrxQty: null,
            PackageIDs: [],
        };
        self.RemovePkgIdx = function (idx) {
            if ((idx > -1) && (idx < self.PackageIDs.length)) {
                self.PackageIDs.splice(idx, 1);
            };
        };
        self.RemovePkg = function (pkgId) {
            var idx = self.PackageIDs.indexOf(pkgId);
            self.RemovePkgIdx(idx);
        };

        return self;
    };

    var makeConfirm = function (id) {
        var self = {
            ID: id,
            PartnerTrxId: null,
            PartnerTrxDt: null,
            PoNumber: null,
            VendorsInvoiceNumber: null,

            pkgId: 0,
            Packages: [],
            actId: 0,
            Actions: []
        };

        self.AddPackage = function (pkgId) {
            function zeroPad(num, numZeros) {
                var n = Math.abs(num);
                var zeros = Math.max(0, numZeros - Math.floor(n).toString().length);
                var zeroString = Math.pow(10, zeros).toString().substr(1);
                if (num < 0) {
                    zeroString = '-' + zeroString;
                }

                return zeroString + n;
            }
            if (!pkgId) {
                self.pkgId++;
                pkgId = 'P_' + self.ID + zeroPad(self.pkgId, 3);
            };

            var pkg = makeConfPackage(pkgId);
            self.Packages.push(pkg);
            return pkg;
        };
        self.AddAction = function () {
            self.actId++;
            var act = makeConfAction(self.actId);
            self.Actions.push(act);
            return act;
        };
        self.DelPackage = function (idx) {
            var pkg = self.Packages[idx];
            $.each(self.Actions, function () {
                this.RemovePkg(pkg.ID);
            })
            self.Packages.splice(idx, 1);
        };
        self.DelAction = function (idx) {
            self.Actions.splice(idx, 1);
        };

        return self;
    };

    var makeConfBatch = function () {
        var self = {
            FileName: null,
            NewFileName: null,
            confId: 0,
            Confirms: [],
        };

        self.AddConfirm = function () {
            self.confId++;
            var conf = makeConfirm(self.confId);
            self.Confirms.push(conf);
            return conf;
        };

        self.DelConfirm = function (idx) {
            if ((idx > -1) && (idx < self.Confirms.length)) {
                self.Confirms.splice(idx, 1);
            };
        };

        return self;
    };

    var batchFromJson = function (jsonBatch) {
        var batch = makeConfBatch();
        batch.FileName = jsonBatch.FileName;

        //$.each(jsonBatch.Confirms, function (idx, jsonConf) {
        angular.forEach(jsonBatch.Confirms, function (jsonConf) {
            var confirm = batch.AddConfirm();
            confirm.PartnerTrxId = jsonConf.PartnerTrxId;
            confirm.PartnerTrxDt = jsonConf.PartnerTrxDt;
            confirm.PoNumber = jsonConf.PoNumber;
            confirm.VendorsInvoiceNumber = jsonConf.VendorsInvoiceNumber;

            //$.each(jsonConf.Packages, function (idx, jsonPkg) {
            angular.forEach(jsonConf.Packages, function (jsonPkg) {
                var pkg = confirm.AddPackage(jsonPkg.ID);
                pkg.ShipDate = jsonPkg.ShipDate;
                pkg.ServiceLevel1 = jsonPkg.ServiceLevel1;
                pkg.TrackingNumber = jsonPkg.TrackingNumber;
                pkg.Weight = jsonPkg.Weight;

            });
            //$.each(jsonConf.Actions, function (idx, jsonAct) {
            angular.forEach(jsonConf.Actions, function (jsonAct) {
                var action = confirm.AddAction();
                action.Shipment = jsonAct.Shipment;
                action.MerchantLineNumber = jsonAct.MerchantLineNumber;
                action.ActionCode = jsonAct.ActionCode;
                action.TrxVendorSKU = jsonAct.TrxVendorSKU;
                action.TrxMerchantSKU = jsonAct.TrxMerchantSKU;
                action.TrxQty = jsonAct.TrxQty;
                angular.forEach(jsonAct.PackageIDs, function (pkgId) {
                    action.PackageIDs.push(pkgId);
                });
            });
        });

        return batch;
    };

    var confService = {
        makeConfBatch: makeConfBatch,
        batchFromJson: batchFromJson
    };

    return confService;
}])

.factory('AckService', ['$q', 'Read', 'OrderCache', function ($q, Read, OrderCache) {

    var makeAckMsg = function (trxID, order, accept) {
        return {
            TrxID: trxID,
            Order: order,
            Accept: accept
        };
    };

    var makeAck = function (TrxSetID) {
        var ack = {
            TrxSetID: TrxSetID,
            AckMsgs: []
        };

        ack.addAckMsg = function (trxID, order, accept) {
            var newAckMsg = makeAckMsg(trxID, order, accept);
            ack.AckMsgs.push(newAckMsg);
            return newAckMsg;
        };

        return ack;
    };

    var makeAckBatch = function (fileName) {
        var batch = {
            FileName: fileName,
            NewFileName: null,
            FAs: []
        };

        batch.addAck = function (trxSetID) {
            var newAck = makeAck(trxSetID);
            batch.FAs.push(newAck);
            return newAck;
        }

        return batch;
    };

    var batchFromJson = function (jsonBatch) {
        var batch = makeAckBatch(jsonBatch.FileName);

        angular.forEach(jsonBatch.FAs, function (jsonAck) {
            var ack = batch.addAck(jsonAck.TrxSetID);

            angular.forEach(jsonAck.AckMsgs, function (jsonAckMsg) {
                var ackMsg = ack.addAckMsg(jsonAckMsg.TrxID, jsonAckMsg.Order, jsonAckMsg.Accept)
            });

        });

        return batch;
    };

    var batchFromOrder = function (orderBatch) {
        var batch = makeAckBatch();
        if (orderBatch) {

            if (orderBatch.BatchNumber) {
                batch.NewFileName = orderBatch.BatchNumber.trim() + ".FA";
            };

            angular.forEach(orderBatch.Orders, function (jsonOrder) {
                var ack = batch.addAck(orderBatch.BatchNumber);
                var ackMsg = ack.addAckMsg(jsonOrder.OrderID, true, true);
            });
        };
        return batch;
    };

    var loadBatch = function (fileName, orderCacheKey) {
        var delay = $q.defer();

        if (orderCacheKey) {
            var cachedOrder = OrderCache.get(orderCacheKey);
            var batch = batchFromOrder(cachedOrder);
            if (batch) {
                delay.resolve(batch);
            } else {
                delay.resolve(null);
            };
        } else if (fileName) {
            Read.acks(fileName).then(
                function (jsonBatch) {
                    var batch = batchFromJson(jsonBatch);
                    delay.resolve(batch);
                },
                function (err) {
                    delay.reject(err);
                }
            );
        } else {
            delay.resolve(null);
        };
        return delay.promise;
    };

    var ackService = {
        makeAckBatch: makeAckBatch,
        batchFromJson: batchFromJson,
        loadBatch: loadBatch
    };

    return ackService;
} ])

;