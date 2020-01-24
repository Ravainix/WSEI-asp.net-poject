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
        if (response.ok) {
            response.json()
                .then(json => resolve(json))
        } else {
            reject(response)
        }
    })
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