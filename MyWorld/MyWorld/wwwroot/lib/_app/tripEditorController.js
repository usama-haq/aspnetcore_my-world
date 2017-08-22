!function(){"use strict";function t(t){if(t&&t.length>0){var o=_.map(t,function(t){return{lat:t.latitude,long:t.longitude,info:t.name}});travelMap.createMap({stops:o,selector:"#map",currentStop:1,initialZoom:3})}}angular.module("app-trips").controller("tripEditorController",function(o,n){var e=this;e.tripName=o.tripName,e.stops=[],e.errorMessage="",e.isBusy=!0,e.newStop={};var s="/api/trips/"+e.tripName+"/stops";n.get(s).then(function(o){angular.copy(o.data,e.stops),t(e.stops)},function(t){e.errorMessage="Failed to Load Stops."}).finally(function(){e.isBusy=!1}),e.addStop=function(){e.isBusy=!0,e.errorMessage="",n.post(s,e.newStop).then(function(o){e.stops.push(o.data),t(e.stops),e.newStop={}},function(t){e.errorMessage="Failed to add new Stop."}).finally(function(){e.isBusy=!1})}})}();