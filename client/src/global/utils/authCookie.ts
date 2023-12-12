const cookieName = "ticket_token";

export const setAuthCookie = (token: string) => {
  document.cookie = `${cookieName}=${token}; path=/; samesite=stric`;
};

export const getAuthCookie = () => {
  return getC(cookieName);
}

export const deleteAuthCookie = () => {
  document.cookie = `${cookieName}=; max-age=0`;
}

function getC(cname: string) {
  let name = cname + "=";
  let ca = document.cookie.split(';');
  for(let i = 0; i < ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}