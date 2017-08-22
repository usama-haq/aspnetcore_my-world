/// <binding AfterBuild='minify' />
var gulp = require('gulp');
var uglify = require('gulp-uglify');
var ngAnotate = require('gulp-ng-annotate');

gulp.task('minify', function () {
    return gulp.src("wwwroot/js/*.js")
        .pipe(ngAnotate())
        .pipe(uglify())
        .pipe(gulp.dest("wwwroot/lib/_app"));
});