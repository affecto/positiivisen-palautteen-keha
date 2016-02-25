/// <binding BeforeBuild='sass' Clean='clean' ProjectOpened='default' />

"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    sass = require("gulp-sass"),
    //autoprefixer = require("gulp-autoprefixer"),
    plumber = require("gulp-plumber");

var paths = {
    webroot: "./",
    npmSource: "./node_modules/",
    libTarget: "./Scripts/"
};

paths.js = paths.webroot + "Scripts/**/*.js";
paths.minJs = paths.webroot + "Scripts/**/*.min.js";
paths.css = paths.webroot + "Content/**/*.css";
paths.minCss = paths.webroot + "Content/**/*.min.css";
paths.concatJsDest = paths.webroot + "Scripts/site.min.js";
paths.concatCssDest = paths.webroot + "Content/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

var libsToMove = [
   paths.npmSource + "/angular2/bundles/angular2-polyfills.js",
   paths.npmSource + "/systemjs/dist/system.src.js",
   paths.npmSource + "/rxjs/bundles/Rx.js",
   paths.npmSource + "/angular2/bundles/angular2.dev.js",
   paths.npmSource + "/angular2/bundles/router.dev.js",
   paths.npmSource + "/jquery/dist/jquery.js",
   paths.npmSource + "/isotope-layout/dist/isotope.pkgd.js"
];

gulp.task("moveToLib", function () {
    return gulp.src(libsToMove).pipe(gulp.dest(paths.libTarget));
});


// scss compile
gulp.task("sass", function() {
    gulp.src("./Content/scss/main.scss")
    .pipe(plumber())
    .pipe(sass().on("error", sass.logError))
    .pipe(gulp.dest("./Content/"));
});

// watch task
gulp.task("watch", function() {
    gulp.watch("./Content/scss/**/*.scss", ["sass"]);
});

// Default task
gulp.task("default", ["sass", "watch"]);
