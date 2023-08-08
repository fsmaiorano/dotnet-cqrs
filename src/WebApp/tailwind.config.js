/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml',
    ],
    theme: {
        extend: {
            gridTemplateColumns: {
                app: 'minmax(18rem, 20rem) 1fr',
                profile: 'max-content 1fr min-content',
                form: 'minmax(7.5rem, 17.5rem) minmax(25rem, 1fr) minmax(0, 15rem)',
            },
            borderWidth: {
                6: '6px',
            },
            colors: {
                'violet': {
                    25: '#fcfaff',
                }
            },
            backgroundImage: {
                'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
                'gradient-conic':
                    'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))',
            },
        },
    },
    plugins: [],
}

//1rem = 16px