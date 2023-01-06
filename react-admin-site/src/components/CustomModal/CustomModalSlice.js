const { createSlice } = require("@reduxjs/toolkit");

const init = {
    isShowModal: false,
    title: "",
    content: "",
    orderList: []
}

const modal = createSlice({
    name: "modal",
    initialState: init,
    reducers: {
        openModal: (state, action) => {
            const { title,  content, orderList } = action.payload;

            state.isShowModal = true;
            state.title = title;
            state.content = content;
            state.orderList = orderList;
        },
        closeModal: (state, action) => {
            state.isShowModal = false
        }
    },
})

const { reducer, actions } = modal;
export const { openModal, closeModal } = actions;
export default reducer;