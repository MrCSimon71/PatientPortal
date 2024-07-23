
const toggle = document.getElementById("headerToggle");
const headerpd = document.getElementById("header");
const sidebar = document.getElementById("sidebar");
const bodypd = document.getElementById('theBody');

if (toggle && sidebar && bodypd && headerpd) {
  toggle.addEventListener("click", () => {

    // 1. Hide/collapse any visible submenus
    hideSubmenus();

    // 2. Collapse all sidebar dropdown menuitems
    collapseDropdownLinks();

    // 3. Remove/disable toggle-bility
    const dropdownLinks = document.querySelectorAll('.dropdown-link');

    dropdownLinks.forEach((a) => {
      if (a.hasAttribute('data-bs-toggle')) {
        a.removeAttribute('data-bs-toggle');
      } else {
        a.setAttribute('data-bs-toggle', 'collapse');
      }
    });

    // 4. Adjust elements to only show buttons and adjust main content
    sidebar.classList.toggle("show");
    toggle.classList.toggle("bx-arrow-from-right");
    toggle.classList.toggle("bx-arrow-from-left");
    bodypd.classList.toggle("defaultLayout-body-pd");
    headerpd.classList.toggle("defaultLayout-body-pd");
  });
}

const menuLinks = document.querySelectorAll('.nav-link-top-level');

menuLinks.forEach((s) => s.addEventListener("click", () => {
  if (menuLinks) {

    const isDropdownLink = s.classList.contains('dropdown-link');
    const isExpanded = s.parentElement?.classList.contains('expanded');

    // 1. Hide/collapse any visible submenus
    hideSubmenus();

    // 2. Collapse all sidebar dropdown menuitems
    collapseDropdownLinks();

    if (isDropdownLink) {
      s.parentElement?.classList.add(isExpanded ? 'collapsed' : 'expanded');
    }
  }
}));

function hideSubmenus() {
  const submenus = document.querySelectorAll('.nav-submenu');
  submenus.forEach((m) => m.classList.remove('show'));
}

function collapseDropdownLinks() {
  const sidebarDropdowns = document.querySelectorAll(".sidebar-dropdown");

  sidebarDropdowns.forEach((m) => {
    m.classList.remove('expanded');
    m.classList.add('collapsed');
  });
}


