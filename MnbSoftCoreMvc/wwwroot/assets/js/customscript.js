document.querySelectorAll(".numeric-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        if (!/^[0-9]$/.test(e.key)) {
            e.preventDefault();
        }
    });
});
document.querySelectorAll(".numeric-dot-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        // Allow digits (0-9) and only one dot
        if (!/^[0-9.]$/.test(e.key)) {
            e.preventDefault();
        }

        // Prevent multiple dots
        if (e.key === "." && element.value.includes(".")) {
            e.preventDefault();
        }
    });
});
document.querySelectorAll(".alpha-dot-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        // Allow alphabets (A-Z, a-z) and dot (.)
        if (!/^[a-zA-Z. ]$/.test(e.key)) {
            e.preventDefault();
        }

        // Prevent multiple consecutive dots
        if (e.key === "." && element.value.slice(-1) === ".") {
            e.preventDefault();
        }
    });
});
document.querySelectorAll(".alpha-numeric-dot-comma-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        // Allow alphabets, dot, and comma
        if (!/^[a-zA-Z0-9.,\-_ ]$/.test(e.key)) {
            e.preventDefault();
        }

        // Prevent multiple consecutive dots or commas
        if (([".", ",", "-", "_"].includes(e.key)) && element.value.slice(-1) === e.key) {
            e.preventDefault();
        }
    });
});
document.querySelectorAll(".alpha-dot-comma-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        // Allow alphabets, dot, comma, dash, underscore, and space
        if (!/^[a-zA-Z.,\-_ ]$/.test(e.key)) {
            e.preventDefault();
        }

        // Prevent multiple consecutive dot, comma, dash, or underscore
        if (([".", ",", "-", "_"].includes(e.key)) && element.value.slice(-1) === e.key) {
            e.preventDefault();
        }
    });
});
document.querySelectorAll(".website-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        const key = e.key;

        // ✅ Allow only alphabets, numbers, dot, and dash
        if (!/^[a-zA-Z0-9.-]$/.test(key)) {
            e.preventDefault();
            return;
        }

        // ❌ Prevent starting with dot or dash
        if ((key === "." || key === "-") && element.value.length === 0) {
            e.preventDefault();
            return;
        }

        // ❌ Prevent consecutive dot or dash
        if ((key === "." || key === "-") && element.value.slice(-1) === key) {
            e.preventDefault();
            return;
        }
    });
});

document.querySelectorAll(".email-only").forEach(function (element) {
    element.addEventListener("keypress", function (e) {
        // Allow alphabets, numbers, dot, underscore, hyphen, and @@
        if (!/^[a-zA-Z0-9@@._-]$/.test(e.key)) {
            e.preventDefault();
        }

        // Prevent starting with dot, hyphen, underscore, or @@
        if (/^[.@@_-]$/.test(e.key) && element.value.length === 0) {
            e.preventDefault();
        }

        // Allow only one @@ symbol
        if (e.key === "@@" && element.value.includes("@@")) {
            e.preventDefault();
        }

        // Prevent consecutive dots
        if (e.key === "." && element.value.slice(-1) === ".") {
            e.preventDefault();
        }
    });
});
//document.querySelectorAll(".alfa-numeric-only").forEach(function (element) {
//    element.addEventListener("keypress", function (e) {
//        // Allow alphabets (A-Z, a-z) and numbers (0-9)
//        if (!/^[a-zA-Z0-9]$/.test(e.key)) {
//            e.preventDefault();
//        }
//    });
//});
//(function () {
//    // Intercept ALL <a> clicks
//    document.addEventListener("click", function (e) {
//        const link = e.target.closest("a");
//        if (!link) return;

//        const url = link.getAttribute("href");

//        // Ignore empty, hash, external links
//        if (!url || url.startsWith("#") || url.startsWith("http")) return;

//        e.preventDefault();
//        navigate(url);
//    });

//    // Navigation function
//    function navigate(url, addHistory = true) {
//        fetch(url, { headers: { "X-Requested-With": "XMLHttpRequest" } })
//            .then(res => res.text())
//            .then(html => {
//                const parser = new DOMParser();
//                const doc = parser.parseFromString(html, "text/html");

//                // Replace only <body> content
//                document.body.innerHTML = doc.body.innerHTML;

//                if (addHistory) {
//                    history.pushState({}, "", url);
//                }
//            })
//            .catch(err => console.error("Navigation error:", err));
//    }

//    // Handle browser Back/Forward
//    window.addEventListener("popstate", function () {
//        navigate(location.href, false);
//    });
//})();