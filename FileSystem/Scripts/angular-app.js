(function () {
    var app = angular.module('FileSystem', []);


    app.controller('FileSystemController', ['$http', function ($http) {
        this.Model = { CurrentPath: ""};
        var Ctrl = this;

        this.GetDirectoryData = function (directory) {
            var path = "";
            if (directory == null) {
                path = "";
            } else if (directory === "") {
                var cutIndex = this.Model.CurrentPath.lastIndexOf("\\", this.Model.CurrentPath.length - 2) ;
                path = this.Model.CurrentPath.substring(0, cutIndex + 1);
            } else {
                path = this.Model.CurrentPath + directory + "\\";
            }

            $http.post('/Home/GetDirectoryData', { path: path }).success(function (data) {
                Ctrl.Model = data;
            });
        }

        this.HasParent = function () {
            var index = Ctrl.Model.CurrentPath.indexOf("\\");
            return index !== Ctrl.Model.CurrentPath.length - 1;
        }

    }]);
})();