import { createRouter, createWebHistory, RouteRecordRaw, RouteLocation } from "vue-router";
import store from "@/store";

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'home',
    component: () => import('@/pages/HomePage.vue'),
    meta: {
      title: 'Home',
      layout: 'HomeLayout',
      requiresAuth: false,
      metaTags: [
        {
          name: 'description',
          content: 'The Home page of our example app.'
        },
        {
          property: 'og:description',
          content: 'The Home page of our example app.'
        }
      ]
    }
  },
  {
    path: "/patients",
    name: 'patients',
    component: () => import("@/views/DefaultView.vue"),
    children: [
      {
        path: '',
        name: "patient-list",
        component: () => import("@/components/patients/PatientList.vue")
      },
      {
        path: 'add',
        name: "patient-add",
        component: () => import("@/components/patients/PatientAdd.vue")
      },
      {
        path: ':id',
        name: 'patient-details',
        component: () => import("@/components/patients/PatientDetails.vue")
      },
      {
        path: ':id/edit',
        name: 'patient-edit',
        component: () => import("@/components/patients/PatientEdit.vue")
      },
    ]
  },
  {
    path: "/users",
    name: 'users',
    component: () => import("@/views/DefaultView.vue"),
    children: [
      {
        path: '',
        name: "user-list",
        component: () => import("@/components/users/UserList.vue")
      },
      {
        path: 'add',
        name: "user-add",
        component: () => import("@/components/users/UserAdd.vue")
      },
      {
        path: ':id',
        name: 'user-details',
        component: () => import("@/components/users/UserDetails.vue")
      },
      {
        path: ':id/edit',
        name: 'user-edit',
        component: () => import("@/components/users/UserEdit.vue")
      },
    ]
  },
  {
    path: '/sign-in',
    name: 'SignIn',
    component: () => import('@/pages/SignIn.vue'),
    meta: {
      title: 'Sign In',
      layout: 'AuthLayout',
      requiresAuth: false
    },
  },
  {
    path: '/forgot-password',
    name: 'ForgotPassword',
    component: () => import('@/pages/ForgotPassword.vue'),
    meta: {
      title: 'Forgot Password',
      layout: 'AuthLayout',
      requiresAuth: false
    },
  },
  {
    path: '/reset-password',
    name: 'ResetPassword',
    component: () => import('@/pages/ResetPassword.vue'),
    meta: {
      title: 'Reset Password',
      layout: 'AuthLayout',
      requiresAuth: false
    },
  },
  {
    path: '/:catchAll(.*)*',
    name: "PageNotFound",
    meta: {
      title: 'Page Not Found',
      layout: 'HomeLayout',
    },
    component: () => import('@/pages/PageNotFound.vue'),
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

router.beforeEach(async (to: RouteLocation, from: RouteLocation, next) => {

  setPageTitle(to, from);

  if (to.name == 'PageNotFound') {
    next();
  }
  else if (to.path == '/') {
    next('/sign-in');
  } else if (to.meta.requiresAuth == null || to.meta.requiresAuth == true) {

    await isAuthenticated()
      .then(result => {
        if (result === true) {
          store.dispatch('Auth/setSessionActivity');
          next();
        } else {
          store.dispatch('Auth/logOut');
          next('/sign-in');
        }       
      }).catch(error => {
        return false;
      });

  } else {
    next();
  }
});

async function isAuthenticated() : Promise<boolean> {
  const isAuthenicated = await store.dispatch('Auth/isSessionValid')
    .then(result => {
      return result;
    }).catch (error => {
      return false;
    });

  return isAuthenicated;
}

function setPageTitle(to: RouteLocation, from: RouteLocation) {

  // This goes through the matched routes from last to first, finding the closest route with a title.
  // e.g., if we have `/some/deep/nested/route` and `/some`, `/deep`, and `/nested` have titles,
  // `/nested`'s will be chosen.
  const nearestWithTitle = to.matched.slice().reverse().find(r => r.meta && r.meta.title);

  // Find the nearest route element with meta tags.
  const nearestWithMeta = to.matched.slice().reverse().find(r => r.meta && r.meta.metaTags);

  const previousNearestWithMeta = from.matched.slice().reverse().find(r => r.meta && r.meta.metaTags);

  // If a route with a title was found, set the document (page) title to that value.
  if (nearestWithTitle) {
    document.title = nearestWithTitle.meta.title as string;
  } else if (previousNearestWithMeta) {
    document.title = previousNearestWithMeta.meta.title as string;
  }

  // Remove any stale meta tags from the document using the key attribute we set below.
  Array.from(document.querySelectorAll('[data-vue-router-controlled]')).map(el => el.parentNode?.removeChild(el));

  // Skip rendering meta tags if there are none.
  if (!nearestWithMeta) return;

  // Turn the meta tag definitions into actual elements in the head.
  if (nearestWithMeta.meta.metaTags != null) {

    const metaTags = nearestWithMeta.meta.metaTags as any;

    metaTags.map((tagDef: { [key: string]: string }) => {
      const tag = document.createElement('meta');

      Object.keys(tagDef).forEach(key => {
        tag.setAttribute(key, tagDef[key]);
      });

      // We use this to track which meta tags we create so we don't interfere with other ones.
      tag.setAttribute('data-vue-router-controlled', '');

      return tag;
    })

    // Add the meta tags to the document head.
    .forEach((tag: any) => document.head.appendChild(tag));
  }
}

export default router;
