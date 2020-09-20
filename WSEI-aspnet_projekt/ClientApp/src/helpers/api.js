import AuthorizeService from '../components/api-authorization/AuthorizeService'

export const get = async (url) => {
    const token = await AuthorizeService.getAccessToken()
    return new Promise(
        (resolve, reject) => {
            fetch(url, {
                headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
            })
                .then(response => response.json())
                .then(json => resolve(json))
                .catch(err => console.error(err))
        }
    )
}

const apiCall = async (url, method, body, resolve, reject) => {
    const token = await AuthorizeService.getAccessToken()
    fetch(url, {
        method: method,
        headers: {
            "Content-Type": 'application/json; charset=utf-8',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(body)
    })
    .then(response => {
        response.ok
          ? resolve(handleResponseStatusAndContentType(response))
          : reject(handleResponseStatusAndContentType(response));
    })
    .catch(err => console.error(err))
}

export const put = (url, body) =>
    new Promise(
        (resolve, reject) => apiCall(url, 'PUT', body, resolve, reject)
    )

export const post = (url, body) =>
    new Promise(
        (resolve, reject) => apiCall(url, 'POST', body, resolve, reject)
    )

export const destroy = async (url) => {
    const token = await AuthorizeService.getAccessToken()
    return new Promise(
        (resolve, reject) => {
            fetch(url, {
                method: 'DELETE',
                headers: {
                    "Content-Type": 'application/json; charset=utf-8',
                    'Authorization': `Bearer ${token}`
                }
            }).then(response => {
                if (response.ok) {
                    resolve(response)
                } else {
                    reject(response)
                }
            })
        }
    )
}

const handleResponseStatusAndContentType = (response) => {
    const contentType = response.headers.get('content-type');
  
    if (response.status === 401) throw new Error('Request was not authorized.');
  
    if (contentType === null) return new Promise(() => null);
    else if (contentType.startsWith('application/json;')) return response.json();
    else if (contentType.startsWith('text/plain;')) return response.text();
    else throw new Error(`Unsupported response content-type: ${contentType}`);
  }